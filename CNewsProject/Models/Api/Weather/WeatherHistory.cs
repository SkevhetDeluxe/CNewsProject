using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static System.Console;

namespace CNewsProject.Models.Api.Weather
{
    public class WeatherHistory
    {
        private readonly string _apiKey;
        private readonly string _baseUrl = "https://history.openweathermap.org/data/2.5/history/city";

        public WeatherHistory(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> GetWeatherHistoryAsync(double latitude, double longitude, DateTime startDate, DateTime endDate)
        {
            // Convert DateTime to UNIX timestamp
            long startTimestamp = ((DateTimeOffset)startDate).ToUnixTimeSeconds();
            long endTimestamp = ((DateTimeOffset)endDate).ToUnixTimeSeconds();

            // Construct the full API URL
            string url = $"{_baseUrl}?lat={latitude}&lon={longitude}&type=hour&start={startTimestamp}&end={endTimestamp}&appid={_apiKey}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send the HTTP GET request
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read and return the response content
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        return jsonResponse;
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }
        }

        public void ParseWeatherHistory(string jsonResponse)
        {
            JObject weatherData = JObject.Parse(jsonResponse);

            // Example: Get temperature data
            var temperatureData = weatherData["list"]?["main"]?["temp"];

            if (temperatureData != null)
            {
                WriteLine($"Temperature: {temperatureData}");
            }
        }
    }
}
