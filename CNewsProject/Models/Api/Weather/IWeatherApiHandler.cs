namespace CNewsProject.Models.Api.Weather
{
    public interface IWeatherApiHandler
    {
        public Task<WeatherStats> GetWeatherAsync(string latVal, string longVal);

    }
}
