using CNewsProject.Models.Api.CurrencyExchangeRate;
using System.Security.Claims;
using static CNewsProject.StaticTempData.CTempData;

namespace CNewsProject.ViewComponents.API.Exchange
{
    public class ExchangeAllViewComponent : ViewComponent
    {
        private readonly ICurrencyExchangeRateService _ExchangeService;

        public ExchangeAllViewComponent(ICurrencyExchangeRateService exch)
        {
            _ExchangeService = exch;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (ExchangeRates.DateUpdated != DateOnly.FromDateTime(DateTime.Now))
            {
                await _ExchangeService.GetExchangeRatesAsync();
            }
            
            return View();
        }
    }
}
