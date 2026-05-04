namespace WeatherApiExercise.Models;

public class WeatherModel: IBaseModel
{
    public Guid Id { get; set; }
    public string CityName { get; set; }
    public double CurrentTemp { get; set; }
    public DateTime CurrentTime { get; set; }
    public TempStatus Status { get; set; }

}

public abstract class TempStatus
{
    public abstract string DisplayName { get;}

    public string GetDisplayName()
    {
        return DisplayName;
    }
}

public class FreezingStatus: TempStatus
{
    public override string DisplayName => "Freezing";
}

public class ColdStatus: TempStatus
{
    public override string DisplayName => "Cold";
}

public class MildStatus: TempStatus
{
    public override string DisplayName => "Mild";
}

public class WarmStatus: TempStatus
{
    public override string DisplayName => "Warm";
}

public class HotStatus: TempStatus
{
    public override string DisplayName => "Hot";
}