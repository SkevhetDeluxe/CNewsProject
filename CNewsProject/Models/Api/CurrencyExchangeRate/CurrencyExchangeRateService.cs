using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static CNewsProject.StaticTempData.CTempData;

namespace CNewsProject.Models.Api.CurrencyExchangeRate
{
    public class CurrencyExchangeRateService  : ICurrencyExchangeRateService 
    {
		private readonly ApplicationDbContext _dbContext;

		public CurrencyExchangeRateService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
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
            ExchangeRates.DateUpdated = DateTime.Now;
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
				exchangeRates.DateUpdated = DateTime.Now;
				_dbContext.ExchangeRates.Add(exchangeRates);
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}
