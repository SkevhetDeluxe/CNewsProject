namespace CNewsProject.Models.Api.Weather
{
    public interface IWeatherApiHandler
    {
        public Task<WeatherStats> GetPositionAsync(string Place);
        public Task<WeatherStats> GetPositionAsync(); // Default
        public Task<WeatherStats> GetWeatherAsync(string latVal, string longVal);
        public Task<WeatherStats> GetWeatherAsync(); // Default overload MOTHERFUCKER!

       
    }
}
