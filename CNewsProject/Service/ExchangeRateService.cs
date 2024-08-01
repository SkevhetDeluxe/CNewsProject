using CNewsProject.Models.Api.ExchangeRates;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CNewsProject.Service
{
    public class ExchangeRateService:IExchangeRateService
    {
        private readonly HttpClient _httpClient;

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExchangeRates> GetExchangeRatesAsync()
        {
            var response = await _httpClient.GetAsync("https://api.exchangerate-api.com/v4/latest/USD"); // Replace with actual API endpoint
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var exchangeRates = JsonSerializer.Deserialize<ExchangeRates>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Assuming the API response directly matches the model structure.
            return exchangeRates;
        }
    }
}
