using System.Web;

namespace WeatherApiExercise.Handlers.Services;

public static class UriExtensions
{
    public static string AddParameter(this string url, string paramName, string paramValue)
    {
        var uriBuilder = new UriBuilder(url);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query[paramName] = paramValue;
        uriBuilder.Query = query.ToString();

        return uriBuilder.ToString();
    }
}