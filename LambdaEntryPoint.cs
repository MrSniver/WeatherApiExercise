namespace WeatherApiExercise;

public class LambdaEntryPoint :

    Amazon.Lambda.AspNetCoreServer.APIGatewayHttpApiV2ProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder
            .UseStartup<Startup>();
    }
    
    protected override void Init(IHostBuilder builder)
    {
    }
}