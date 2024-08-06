using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CNewsProject.Models.Api.CurrencyExchangeRate
{
    public class CurrencyExchangeRateService  : ICurrencyExchangeRateService 
    {
        public async Task<Rates> GetExchangeRatesAsync()
        {
            Rates? exchangeRates;

            using (HttpClient _httpClient = new())
            {
                var response = await _httpClient.GetAsync("https://api.exchangerate-api.com/v4/latest/SEK");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);

                exchangeRates = jsonObject["rates"].ToObject<Rates>();
            }
            // Assuming the API response directly matches the model structure.
            return exchangeRates ?? new Rates();
        }
    }
}
