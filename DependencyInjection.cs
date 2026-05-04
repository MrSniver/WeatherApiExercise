using System.Reflection;
using WeatherApiExercise.Handlers.Services;
using WeatherApiExercise.Handlers.Services.Interfaces;

public static class DependencyInjection
{
    public static IServiceCollection AddDepencencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
        services.AddScoped<IWeatherService, WeatherService>();
        services.AddScoped<IWeatherApiIntegrationService, WeatherApiIntegrationService>();

        return services;   
    }
}