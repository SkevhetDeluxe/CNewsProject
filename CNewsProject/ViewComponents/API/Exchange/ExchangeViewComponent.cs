using CNewsProject.Models.Api.CurrencyExchangeRate;
using System.Security.Claims;

namespace CNewsProject.ViewComponents.API.Exchange
{
    public class ExchangeViewComponent : ViewComponent
    {
        private readonly ICurrencyExchangeRateService _ExchangeService;

        public ExchangeViewComponent(ICurrencyExchangeRateService exch)
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
