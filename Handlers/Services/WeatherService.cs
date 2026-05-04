using WeatherApiExercise.Handlers.Services.Interfaces;
using WeatherApiExercise.Models;

namespace WeatherApiExercise.Handlers.Services;

public class WeatherService: IWeatherService
{
    public string GetWeatherStatus(double temp)
    {
        if( temp <= 0)
            return new FreezingStatus().GetDisplayName();
        else if( temp > 0 && temp <= 10)
            return new ColdStatus().GetDisplayName();
        else if( temp > 10 && temp <= 20)
            return new MildStatus().GetDisplayName();
        else if( temp > 20 && temp <= 30)
            return new WarmStatus().GetDisplayName();
        else if( temp > 30)
            return new HotStatus().GetDisplayName();
        
        return "NoStatus";
    }
}