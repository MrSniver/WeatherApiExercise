using WeatherApiExercise.Models;
using WeatherApiExercise.Models.DTOs;
using System.Text.Json;
using WeatherApiExercise.Handlers.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace WeatherApiExercise.Handlers.Services;

public class WeatherApiIntegrationService: IWeatherApiIntegrationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<ApiConfiguration> _apiConfiguration;
    private readonly IWeatherService _weatherService;

    public WeatherApiIntegrationService(IHttpClientFactory httpClientFactory, IOptions<ApiConfiguration> apiConfiguration, IWeatherService weatherService)
    {
        _httpClientFactory = httpClientFactory;
        _apiConfiguration = apiConfiguration;
        _weatherService = weatherService;
    }

    public async Task<WeatherModelDto> GetByNameAsync(string cityName)
    {
        string geoApiBaseUri = _apiConfiguration.Value.OpenMeteoGeocoding.Url;
        geoApiBaseUri = UriExtensions.AddParameter(geoApiBaseUri, "name", cityName);

        var geoClient = _httpClientFactory.CreateClient("MeteoGeoApi");

        var response = await geoClient.GetAsync(geoApiBaseUri);

        if( !response.IsSuccessStatusCode)
            throw new HttpRequestException($"Meteo Geological API Status Code: {response.StatusCode}");

        var responseText = await response.Content.ReadAsStringAsync();
        var geoResponse = JsonSerializer.Deserialize<GeolocationApiModelDto>(responseText);

        if (geoResponse?.Results == null || geoResponse.Results.Count == 0)
            throw new InvalidOperationException($"City '{cityName}' not found");

        var geoData = geoResponse.Results[0];

        string weatherApiBaseUri = _apiConfiguration.Value.OpenMeteoWeather.Url;
        weatherApiBaseUri = UriExtensions.AddParameter(weatherApiBaseUri, "latitude", geoData.latitude.ToString())
            .AddParameter("longitude", geoData.longitude.ToString())
            .AddParameter("current", "temperature_2m")
            .AddParameter("timezone", "auto");

        var weatherClient = _httpClientFactory.CreateClient("MeteoWeatherApi");
        response = await weatherClient.GetAsync(weatherApiBaseUri);

        if( !response.IsSuccessStatusCode)
            throw new HttpRequestException($"Meteo Weather API Status Code: {response.StatusCode}");

        responseText = await response.Content.ReadAsStringAsync();

        var weatherData = JsonSerializer.Deserialize<WeatherApiModelDto>(responseText) ?? throw new InvalidOperationException($"Longitude: {geoData.longitude} or Latitude: {geoData.latitude} are invalid");

        var weatherModel = new WeatherModelDto
        {
            CityName = cityName,
            CurrentTemp = weatherData.current.temperature_2m,
            CurrentTime = weatherData.current.time,
            Status = _weatherService.GetWeatherStatus(weatherData.current.temperature_2m)
        };

        return weatherModel;
    }
}