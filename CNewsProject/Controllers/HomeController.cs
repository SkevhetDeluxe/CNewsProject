using CNewsProject.Models;
using CNewsProject.Models.Api.Weather;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;



namespace CNewsProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherApiHandler _weatherApiHandler;

        public HomeController(ILogger<HomeController> logger, IWeatherApiHandler weatherApiHand)
        {
            _weatherApiHandler = weatherApiHand;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            WeatherStats model = await _weatherApiHandler.GetWeatherAsync("58.25", "15.35");
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


