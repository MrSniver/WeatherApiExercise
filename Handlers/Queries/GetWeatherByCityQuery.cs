using AutoMapper;
using MediatR;
using WeatherApiExercise.Handlers.Services.Interfaces;
using WeatherApiExercise.Models.DTOs;

namespace WeatherApiExercise.Handlers;

public class GetWeatherByCityQuery: IRequest<WeatherModelDto>
{
    public string CityName { get; set; }
    public GetWeatherByCityQuery(string cityName) { CityName = cityName;}
}

public class GetWeatherByCityQueryHandler: IRequestHandler<GetWeatherByCityQuery, WeatherModelDto>
{
    private readonly IWeatherApiIntegrationService _weatherApiIntegrationService;
    private readonly IMapper _mapper;

    public GetWeatherByCityQueryHandler(IWeatherApiIntegrationService weatherApiIntegrationService, IMapper mapper)
    {
        _weatherApiIntegrationService = weatherApiIntegrationService;
        _mapper = mapper;
    }

    public async Task<WeatherModelDto> Handle(GetWeatherByCityQuery request, CancellationToken cancellationToken)
    {
        var weatherEntity = await _weatherApiIntegrationService.GetByNameAsync(request.CityName);

        return _mapper.Map<WeatherModelDto>(weatherEntity);
    }
}