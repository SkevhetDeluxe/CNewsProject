using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using Newtonsoft.Json.Linq;

namespace CNewsFunctions
{
    public class ExchangeRateArchived
    {
        private readonly ILogger _logger;

        public ExchangeRateArchived(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExchangeRateArchived>();
        }

        [Function("ExchangeRateArchived")]
        public async Task Run([TimerTrigger("0 0 3 * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            var connString = "DefaultEndpointsProtocol=https;AccountName=cnewsstorage;AccountKey=42s4C494d16TS+Ww3wwbWFcx3Nn2SuAsL6aJTsbfLnZqoND5gJ6O69MGdzSX69h6YrQTMgyBJ0t++AStJI5xcA==;EndpointSuffix=core.windows.net";
            TableServiceClient tableServiceClient = new TableServiceClient(connString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: "ExchangeRateArchived");

            tableClient.CreateIfNotExists();

            string apiUrl = "https://api.exchangerate-api.com/v4/latest/SEK";
            using HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(responseBody);

            decimal usdRate = jsonObject["rates"]["USD"].Value<decimal>();
            decimal eurRate = jsonObject["rates"]["EUR"].Value<decimal>();
            decimal gbpRate = jsonObject["rates"]["GBP"].Value<decimal>();

            if (usdRate <= 0 || eurRate <= 0 || gbpRate <= 0)
            {
                _logger.LogError("Invalid exchange rate data received.");
                return;
            }

            var ratesForArchive = new RatesForArchive(DateTime.UtcNow)
            {
                USD = usdRate,
                EUR = eurRate,
                GBP = gbpRate
            };

            await tableClient.AddEntityAsync(ratesForArchive);

            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus != null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
