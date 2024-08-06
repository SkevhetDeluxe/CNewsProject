namespace CNewsProject.ViewComponents.API.Weather
{
    public class WeatherViewComponent : ViewComponent
    {
        private readonly IWeatherApiHandler _weatherHandler;

        public WeatherViewComponent(IWeatherApiHandler weath)
        {
            _weatherHandler = weath;
        }

        public IViewComponentResult Invoke()
        {
            WeatherStats reportagetOmDetFinaVadret = _weatherHandler.GetWeatherAsync().Result;

            return View(reportagetOmDetFinaVadret);
        }
    }
}
