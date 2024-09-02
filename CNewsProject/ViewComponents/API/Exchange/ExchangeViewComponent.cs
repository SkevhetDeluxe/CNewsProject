using static CNewsProject.StaticTempData.CTempData;

namespace CNewsProject.ViewComponents.API.Exchange
{
    public class ExchangeViewComponent(ICurrencyExchangeRateService exchangeRates, IFillObjectHelper fillHelper) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()  // This should not go ABOVE 300 ms. Right now it AVERAGES at 4784 ms xD
        {
            if (ExchangeRates.DateUpdated != DateOnly.FromDateTime(DateTime.Now))
            {
                await exchangeRates.GetExchangeRatesAsync();
            }
            
            return View();
        }
    }
}
