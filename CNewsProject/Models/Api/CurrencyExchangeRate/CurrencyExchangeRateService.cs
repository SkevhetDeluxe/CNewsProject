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
            // This thing takes a lot of time. We might not be able to solve that.
            // My suggestion is make the ViewComponent load via Javascript and run this asynchronously in the background.
            // So we don't bottleneck the START PAGE
            
            Rates? exchangeRates;

            using (HttpClient httpClient = new())
            {
                var response = await httpClient.GetAsync("https://api.exchangerate-api.com/v4/latest/SEK");
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
