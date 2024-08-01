using CNewsProject.Models.Api.Weather;
using System.Threading.Tasks;

namespace CNewsProject.Service
{
    public interface IWeatherService
    {
        Task<WeatherStats> GetWeatherAsync(string location);
    }
}
