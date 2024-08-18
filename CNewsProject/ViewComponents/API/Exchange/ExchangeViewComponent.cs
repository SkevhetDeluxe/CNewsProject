namespace CNewsProject.ViewComponents.API.Exchange
{
    public class ExchangeViewComponent(ICurrencyExchangeRateService exchangeRates) : ViewComponent
    {
        public IViewComponentResult Invoke()  // This should not go ABOVE 300 ms. Right now it AVERAGES at 4784 ms xD
        {
            var rates = exchangeRates.GetExchangeRatesAsync().Result;

            return View(rates);
        }
    }
}
