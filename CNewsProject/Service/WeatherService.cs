using CNewsProject.Models.Api.Weather;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CNewsProject.Service
{
    public class WeatherService:IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherStats> GetWeatherAsync(string location)
        {
            var response = await _httpClient.GetAsync($"http://api.openweathermap.org/data/2.5/forecast?q={location}&appid=YOUR_API_KEY&units=metric");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var weatherResponse = JsonSerializer.Deserialize<WeatherStats>(responseContent);

            if (weatherResponse?.Hourly?.Time != null && weatherResponse.Hourly.Time.Count > 0)
            {
                var currentTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:00:00Z");
                var currentHourIndex = weatherResponse.Hourly.Time.IndexOf(currentTime);

                if (currentHourIndex >= 0)
                {
                    weatherResponse.CurrentTime = weatherResponse.Hourly.Time[currentHourIndex];
                    weatherResponse.CurrentTemperature = weatherResponse.Hourly.Temperature_2m[currentHourIndex];
                }
            }

            return weatherResponse;
        }
    }
}
