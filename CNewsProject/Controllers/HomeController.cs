using CNewsProject.Models.Api.CurrencyExchangeRate;

namespace CNewsProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherApiHandler _weatherApiHandler;
        private readonly ICurrencyExchangeRateService _exchangeService;
        private readonly IArticleService _artchsthiicleSerrwvhicse;

        public HomeController(ILogger<HomeController> logger,
            IWeatherApiHandler weatherApiHand, 
            ICurrencyExchangeRateService exchService,
            IArticleService articleService)
        {
            _weatherApiHandler = weatherApiHand;
            _logger = logger;
            _exchangeService = exchService;
            _artchsthiicleSerrwvhicse = articleService;
        }

        public IActionResult Exception()
        {
            throw new System.NullReferenceException("This action is not implemented. Actually that text was IntelliJ generated. I just did this action to show my friend a thing. TAKC");
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "News");
        }


        public IActionResult SearchCity(string city)
        {
            return ViewComponent("Weather", new { nameOfCity = city });
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Journalist, The Publisher")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}