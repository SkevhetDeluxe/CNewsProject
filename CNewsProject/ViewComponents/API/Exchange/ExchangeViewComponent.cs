using CNewsProject.StaticTempData;
using static CNewsProject.StaticTempData.ExchangeRateData;

namespace CNewsProject.ViewComponents.API.Exchange
{
    public class ExchangeViewComponent(ICurrencyExchangeRateService exchangeRates, IFillStaticHelper staticHelper) : ViewComponent
    {
        public IViewComponentResult Invoke()  // This should not go ABOVE 300 ms. Right now it AVERAGES at 4784 ms xD
        {
            if (TimeUpdated != DateOnly.FromDateTime(DateTime.Now))
            {
                var rates = exchangeRates.GetExchangeRatesAsync().Result;
                staticHelper.MapRatesToStaticClass(rates);
                TimeUpdated = DateOnly.FromDateTime(DateTime.Now);
            }
            
            return View();
        }
    }
}
