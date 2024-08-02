using Azure.Core;
using Azure;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CNewsProject.Models.Api.CurrencyExchangeRate
{
    public class CurrencyExchangeRateApiHandler : ICurrencyExchangeRateApiHandler
    {
        private readonly HttpClient _client;

        public CurrencyExchangeRateApiHandler(HttpClient client)
        {
            _client = client;
        }

        public async Task<CurrencyExchangeRate> GetExchangeAsync(string country)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://real-time-finance-data.p.rapidapi.com/stock-time-series-source-2?symbol=AAPL&period=1D"),
                Headers =
                {
                    { "x-rapidapi-key", "788a749ec0mshbc966b8c428eee0p1fd99djsn15f6ea339f04" },
                    { "x-rapidapi-host", "real-time-finance-data.p.rapidapi.com" },
                },
            };

            try
            {
                using (var response = await _client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var exchange = JsonSerializer.Deserialize<CurrencyExchangeRate>(body);
                    return exchange;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
    }
}