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

		// This Method AVERAGES at 8754 ms on my laptop guys. I think we might need to investigate how much calculations are done here he-he
		public async Task<IViewComponentResult> InvokeAsync(string? nameOfCity)
        {
            if (nameOfCity.IsNullOrEmpty())
				nameOfCity = "Stockholm";

            nameOfCity = nameOfCity[0].ToString().ToUpper() + nameOfCity.Substring(1);

            GeoLocation Position = new();

            try
            {
                Position = await _weatherHandler.GetPositionAsync(nameOfCity);
            }
            catch
            {
                Position = await _weatherHandler.GetPositionAsync();
                nameOfCity = "Stockholm";
            }
            
            
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
