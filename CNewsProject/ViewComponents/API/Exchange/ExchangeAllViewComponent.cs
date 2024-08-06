using CNewsProject.Models.Api.CurrencyExchangeRate;
using System.Security.Claims;

namespace CNewsProject.ViewComponents.API.Exchange
{
    public class ExchangeAllViewComponent : ViewComponent
    {
        private readonly ICurrencyExchangeRateService _ExchangeService;

        public ExchangeAllViewComponent(ICurrencyExchangeRateService exch)
        {
            _ExchangeService = exch;
        }

        public IViewComponentResult Invoke()
        {
            Rates rates = _ExchangeService.GetExchangeRatesAsync().Result;

            return View(rates);
        }
    }
}
