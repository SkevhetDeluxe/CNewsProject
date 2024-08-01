using CNewsProject.Models.Api.ExchangeRates;
using System.Threading.Tasks;

namespace CNewsProject.Service
{
    public interface IExchangeRateService
    {
        Task<ExchangeRates> GetExchangeRatesAsync();
    }
}
