using AutoMapper;
using WeatherApiExercise.Models;
using WeatherApiExercise.Models.DTOs;

namespace WeatherApiExercise.Mappings;

public class WeatherModelMappingProfile: Profile
{
    public WeatherModelMappingProfile()
    {
        CreateMap<WeatherModel, WeatherModelDto>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetDisplayName()));
        CreateMap<WeatherModelDto, WeatherModel>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => ConvertStringToStatus(src.Status)))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }

    private TempStatus ConvertStringToStatus(string statusString)
    {
        return statusString switch
        {
            "Freezing" => new FreezingStatus(),
            "Cold" => new ColdStatus(),
            "Mild" => new MildStatus(),
            "Warm" => new WarmStatus(),
            "Hot" => new HotStatus(),
            _ => new MildStatus()
        };
    }
}