using CNewsProject.Models.Api.CurrencyExchangeRate;
using System.Security.Claims;
using static CNewsProject.StaticTempData.ExchangeRateData;

namespace CNewsProject.ViewComponents.API.Exchange
{
    public class ExchangeAllViewComponent : ViewComponent
    {
        private readonly ICurrencyExchangeRateService _ExchangeService;
        private readonly IFillStaticHelper _staticHelper;

        public ExchangeAllViewComponent(ICurrencyExchangeRateService exch, IFillStaticHelper staticHelper)
        {
            _ExchangeService = exch;
            _staticHelper = staticHelper;
        }

        public IViewComponentResult Invoke()
        {
            if (TimeUpdated != DateOnly.FromDateTime(DateTime.Now))
            {
                Rates rates = _ExchangeService.GetExchangeRatesAsync().Result;
                _staticHelper.MapRatesToStaticClass(rates);
                TimeUpdated = DateOnly.FromDateTime(DateTime.Now);
            }
            
            return View();
        }
    }
}
