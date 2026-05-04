namespace WeatherApiExercise.Models;

public class ApiConfiguration
{
    public OpenMeteoWeather OpenMeteoWeather { get; set; }
    public OpenMeteoGeocoding OpenMeteoGeocoding { get; set; }
}

public class OpenMeteoWeather
{
    public string Url { get; set; }
}

public class OpenMeteoGeocoding
{
    public string Url { get; set; }
}