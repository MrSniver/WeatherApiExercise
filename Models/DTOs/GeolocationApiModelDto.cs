using System.Text.Json.Serialization;

namespace WeatherApiExercise.Models.DTOs;

public class GeolocationApiModelDto
{
    [JsonPropertyName("results")]
    public List<GeolocationApiResults> Results { get; set; }
}

public class GeolocationApiResults
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string name { get; set; }

    [JsonPropertyName("country")]
    public string country { get; set; }

    [JsonPropertyName("longitude")]
    public double longitude { get; set; }

    [JsonPropertyName("latitude")]
    public double latitude { get; set; }
}