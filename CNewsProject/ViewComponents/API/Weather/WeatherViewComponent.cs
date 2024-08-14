using Microsoft.IdentityModel.Tokens;
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

		
		public IViewComponentResult Invoke(string? nameOfCity)
        {
            if (nameOfCity.IsNullOrEmpty())
				nameOfCity = "Stockholm";

            nameOfCity = nameOfCity[0].ToString().ToUpper() + nameOfCity.Substring(1);

            GeoLocation Position = _weatherHandler.GetPositionAsync(nameOfCity).Result;
            
            string lon = string.Format("{0:F2}", Convert.ToDecimal(Position.lon));
            string lat = string.Format("{0:F2}", Convert.ToDecimal(Position.lat));
            lat = lat.Replace(',','.');
            lon = lon.Replace(',','.');

            WeatherStats reportagetOmDetFinaVadret = _weatherHandler.GetWeatherAsync(lon, lat).Result;

            reportagetOmDetFinaVadret.NameOfCity = nameOfCity;

            return View(reportagetOmDetFinaVadret);
        }
    }
}
