using CNewsProject.Models.Api.CurrencyExchangeRate;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CNewsProject.Service
{
    public class CurrencyExchangeRateService:ICurrencyExchangeRateService
    {
        private readonly HttpClient _httpClient;

        public CurrencyExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CurrencyExchangeRate> GetExchangeRatesAsync()
        {
            var response = await _httpClient.GetAsync("https://api.exchangerate-api.com/v4/latest/USD"); // Replace with actual API endpoint
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var exchangeRates = JsonSerializer.Deserialize<CurrencyExchangeRate>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Assuming the API response directly matches the model structure.
            return exchangeRates;
        }
    }
}
