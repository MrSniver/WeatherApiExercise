using WeatherApiExercise.Models.DTOs;

namespace WeatherApiExercise.Handlers.Services.Interfaces;

public interface IWeatherApiIntegrationService
{
    public Task<WeatherModelDto> GetByNameAsync(string cityName);
}