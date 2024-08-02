using CNewsProject.Models.Api.CurrencyExchangeRate;

namespace CNewsProject.Models.Api.CurrencyExchangeRate
{
    public interface ICurrencyExchangeRateApiHandler
    {
        public Task<CurrencyExchangeRate> GetExchangeAsync(string country);

    }
}
