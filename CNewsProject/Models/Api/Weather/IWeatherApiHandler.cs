
namespace CNewsProject.Models.Api.Weather
{
    public interface IWeatherApiHandler
    {
        public Task<GeoLocation> GetPositionAsync(string Place);
        
        public Task<WeatherStats> GetWeatherAsync(string latVal, string longVal);

        public Task<GeoLocation> GetPositionAsync(); // Default

        public Task<WeatherStats> GetWeatherAsync(); // Default overload MOTHERFUCKER!

        
	}
}
