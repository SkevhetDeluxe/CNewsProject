using Newtonsoft.Json.Linq;

namespace CNewsProject.Controllers;

using static CNewsProject.StaticTempData.ExchangeRateData;

public class LoadExchangeController(ICurrencyExchangeRateService _exchangeService, IFillStaticHelper _fillStaticHelper)
    : Controller
{
    private readonly string _token = "HawiIUdjoAIDJolaijiowjdOIAJDlAJWDNkjnd2ui82doOI";

    #region FTHIS

    public IActionResult LoadViewComponent()
    {
        return ViewComponent("Exchange");
    }

    [HttpGet]
    public IActionResult UpdateExchangeUrl(string token)
    {
        if (token != "HawiIUdjoAIDJolaijiowjdOIAJDlAJWDNkjnd2ui82doOI")
            return Json(new { error = true, message = "Invalid token" });

        Rates source = _exchangeService.GetExchangeRatesAsync().GetAwaiter().GetResult();

        _fillStaticHelper.MapRatesToStaticClass(source);

        return Json(new { updated = "True", USD = USD });
    }
    [HttpPost]
    public async Task<IActionResult> UpdateExchange(HttpRequest request)
    {
        string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        JObject data = JObject.Parse(requestBody);
        RequestData requestData = data.ToObject<RequestData>();

        if (requestData.Token != _token)
            return Json(new { error = true, message = "Invalid token" });

        Rates source = await _exchangeService.GetExchangeRatesAsync();

        _fillStaticHelper.MapRatesToStaticClass(source);

        return Json(new { updated = "True", USD = USD });
    }
    
    public IActionResult ReadStatic()
    {
        return Json(new { USD = USD, EUR = EUR });
    }
    #endregion
    
}

public class RequestData
{
    public string Token { get; set; } = "Undefined";
}
