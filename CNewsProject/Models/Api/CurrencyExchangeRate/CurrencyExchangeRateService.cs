using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static CNewsProject.StaticTempData.CTempData;

namespace CNewsProject.Models.Api.CurrencyExchangeRate
{
    public class CurrencyExchangeRateService(ApplicationDbContext dbContext) : ICurrencyExchangeRateService
    {
	    public async Task GetExchangeRatesAsync()
        {
            Rates? exchangeRates;

            using (HttpClient httpClient = new())
            {
                var response = await httpClient.GetAsync("https://api.exchangerate-api.com/v4/latest/SEK");
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JObject.Parse(responseContent);

                exchangeRates = jsonObject["rates"]?.ToObject<Rates>();
            }
            ExchangeRates = exchangeRates ?? ExchangeRates;
        }

		public async Task StoreExchangeRatesAsync()
		{
			Rates? exchangeRates;

			using (HttpClient httpClient = new())
			{
				var response = await httpClient.GetAsync("https://api.exchangerate-api.com/v4/latest/SEK");
				response.EnsureSuccessStatusCode();

				var responseContent = await response.Content.ReadAsStringAsync();
				var jsonObject = JObject.Parse(responseContent);

				exchangeRates = jsonObject["rates"]?.ToObject<Rates>();
			}

			if (exchangeRates != null)
			{
				dbContext.ExchangeRates.Add(exchangeRates);
				await dbContext.SaveChangesAsync();
			}
		}
	}
}
