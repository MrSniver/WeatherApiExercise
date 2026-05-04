namespace WeatherApiExercise.Models.DTOs;

public class WeatherModelDto
{
    public string CityName { get; set; }
    public double CurrentTemp { get; set; }
    public DateTime CurrentTime { get; set; }
    public string Status { get; set; }
}