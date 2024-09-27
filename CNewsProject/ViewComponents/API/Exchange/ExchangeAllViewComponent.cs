using CNewsProject.Models.Api.CurrencyExchangeRate;
using System.Security.Claims;
using static CNewsProject.StaticTempData.CTempData;

namespace CNewsProject.ViewComponents.API.Exchange
{
    public class ExchangeAllViewComponent(ICurrencyExchangeRateService exch) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (ExchangeRates.DateUpdated != DateOnly.FromDateTime(DateTime.Now))
            {
                await exch.GetExchangeRatesAsync();
            }
            
            return View();
        }
    }
}
