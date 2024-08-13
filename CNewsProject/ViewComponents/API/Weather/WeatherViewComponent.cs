using System.Globalization;

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
            GeoLocation Position = _weatherHandler.GetPositionAsync().Result;
            
            string lon = string.Format("{0:F2}", Convert.ToDecimal(Position.lon));
            string lat = string.Format("{0:F2}", Convert.ToDecimal(Position.lat));
            lat = lat.Replace(',','.');
            lon = lon.Replace(',','.');

            WeatherStats reportagetOmDetFinaVadret = _weatherHandler.GetWeatherAsync(lon, lat).Result;
            return View(reportagetOmDetFinaVadret);
        }
    }
}
