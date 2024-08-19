using System.Threading.Tasks;

namespace CNewsProject.Models.Api.CurrencyExchangeRate
{
    public interface ICurrencyExchangeRateService
    {
        Task<Rates> GetExchangeRatesAsync();

    }
}
