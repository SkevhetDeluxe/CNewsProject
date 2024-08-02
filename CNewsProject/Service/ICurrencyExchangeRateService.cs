using CNewsProject.Models.Api.CurrencyExchangeRate;
using System.Threading.Tasks;

namespace CNewsProject.Service
{
    public interface ICurrencyExchangeRateService
    {
        Task<CurrencyExchangeRate> GetExchangeRatesAsync();
    }
}
