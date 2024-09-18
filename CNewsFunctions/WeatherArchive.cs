using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using static System.Net.WebRequestMethods;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace CNewsFunctions
{
    public class WeatherArchive
    {
        private readonly ILogger log;

        public WeatherArchive(ILoggerFactory loggerFactory)
        {
            log = loggerFactory.CreateLogger<WeatherArchive>();
        }

        [Function("WeatherArchive")]
        public async Task Run([TimerTrigger("0 0 3 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                // Fetch weather data from OpenWeatherMap API
                string apiUrl = "https://archive-api.open-meteo.com/v1/archive?latitude=52.52&longitude=13.41&start_date=2024-09-02&end_date=2024-09-16&hourly=temperature_2m,relative_humidity_2m,precipitation,weather_code,wind_speed_10m,wind_direction_100m&daily=sunrise,sunset";
                using HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the weather data
                var jsonObject = JObject.Parse(responseBody);
                float temperature = jsonObject["main"]["temp"].Value<float>();
                string condition = jsonObject["weather"][0]["description"].Value<string>();

                log.LogInformation($"Fetched weather data: Temperature - {temperature}, Condition - {condition}");

                // Azure Table storage setup
                string connString = "DefaultEndpointsProtocol=https;AccountName=cnewsstorage;AccountKey=42s4C494d16TS+Ww3wwbWFcx3Nn2SuAsL6aJTsbfLnZqoND5gJ6O69MGdzSX69h6YrQTMgyBJ0t++AStJI5xcA==;EndpointSuffix=core.windows.net";
                TableServiceClient tableServiceClient = new TableServiceClient(connString);
                TableClient tableClient = tableServiceClient.GetTableClient("WeatherArchive");

                // Create table if it doesn't exist
                await tableClient.CreateIfNotExistsAsync();

                // Create an entity for the weather data
                var weatherEntity = new WeatherForArchive(DateTime.UtcNow, temperature, condition);

                // Add entity to Azure Table
                await tableClient.AddEntityAsync(weatherEntity);

                log.LogInformation($"Weather data archived successfully at: {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                log.LogError($"Error archiving weather data: {ex.Message}");
            }
        }
    }
}