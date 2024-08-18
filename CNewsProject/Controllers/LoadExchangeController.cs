namespace CNewsProject.Controllers;

public class LoadExchangeController(ICurrencyExchangeRateService _exchangeService) : Controller
{
    public IActionResult LoadViewComponent()
    {
        return ViewComponent("Exchange");
    }
}