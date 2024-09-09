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
            // Log the start of the function execution
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            string apiUrl = "https://api.openweathermap.org/data/2.5/weather?q=Stockholm&appid=YOUR_API_KEY";
            using HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(responseBody);
            float temperature = jsonObject["main"]["temp"].Value<float>();
            string condition = jsonObject["weather"][0]["description"].Value<string>();
            log.LogInformation($"Fetched weather data: Temperature - {temperature}, Condition - {condition}");
            var connString = "DefaultEndpointsProtocol=https;AccountName=cnewsstorage;AccountKey=42s4C494d16TS+Ww3wwbWFcx3Nn2SuAsL6aJTsbfLnZqoND5gJ6O69MGdzSX69h6YrQTMgyBJ0t++AStJI5xcA==;EndpointSuffix=core.windows.net";
            TableServiceClient tableServiceClient = new TableServiceClient(connString);
            TableClient tableClient = tableServiceClient.GetTableClient("WeatherArchive");
            tableClient.CreateIfNotExists();
            var weatherEntity = new WeatherForArchive(DateTime.UtcNow, temperature, condition);
            await tableClient.AddEntityAsync(weatherEntity);
            log.LogInformation($"Weather data archived successfully at: {DateTime.UtcNow}");            
        }
    }
}
