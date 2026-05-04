using System.Text.Json.Serialization;

namespace WeatherApiExercise.Models.DTOs;

public class WeatherApiModelDto
{
    [JsonPropertyName("latitude")]
    public double latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double longitude { get; set; }

    [JsonPropertyName("current")]
    public WeatherApiCurrentData current { get; set; }
}

public class WeatherApiCurrentData
{
    [JsonPropertyName("time")]
    public DateTime time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public double temperature_2m { get; set; }
}