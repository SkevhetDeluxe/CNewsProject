namespace CNewsProject.Controllers;
using static CNewsProject.StaticTempData.ExchangeRateData;
public class LoadExchangeController(ICurrencyExchangeRateService _exchangeService, IFillStaticHelper _fillStaticHelper) 
    : Controller
{
    public IActionResult LoadViewComponent()
    {
        return ViewComponent("Exchange");
    }
    
    public IActionResult UpdateExchange(string token)
    {
        if (token != "HawiIUdjoAIDJolaijiowjdOIAJDlAJWDNkjnd2ui82doOI")
            return Json(new { error = true, message = "Invalid token" });

        Rates source = _exchangeService.GetExchangeRatesAsync().GetAwaiter().GetResult();
        
        _fillStaticHelper.MapRatesToStaticClass(source);
        
        return Json(new { updated = "True", USD = USD});
    }

    public IActionResult ReadStatic()
    {
        return Json(new { USD = USD, EUR = EUR });
    }
}