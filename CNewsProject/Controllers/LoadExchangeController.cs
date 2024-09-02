using Newtonsoft.Json.Linq;

namespace CNewsProject.Controllers;

public class LoadViewComponentController(ICurrencyExchangeRateService _exchangeService)
    : Controller
{
    private readonly string _token = "HawiIUdjoAIDJolaijiowjdOIAJDlAJWDNkjnd2ui82doOI";

    #region FTHIS

    public IActionResult LoadViewComponent(string name)
    {
        return ViewComponent(name);
    }
    
    #endregion
    
}

public class RequestData
{
    public string Token { get; set; } = "Undefined";
}
