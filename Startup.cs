using WeatherApiExercise.Middlewares;
using WeatherApiExercise.Models;

namespace WeatherApiExercise;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDepencencies();
        services.AddHttpClient();
        services.Configure<ApiConfiguration>(Configuration.GetSection("ApiConfigurations"));
        services.AddControllers();
        services.AddLogging();
        services.AddTransient<ExceptionHandlingMiddleware>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}