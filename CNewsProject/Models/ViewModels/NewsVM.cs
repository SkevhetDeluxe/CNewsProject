using CNewsProject.Models.Api.ExchangeRates;
using CNewsProject.Models.Api.Weather;
using CNewsProject.Models.DataBase;

namespace CNewsProject.Models.ViewModels
{
    public class NewsVM
    {
        public WeatherStats WeatherStats { get; set; }
        public ExchangeRates ExchangeRates { get; set; }
        public List<Article> Articles { get; set; }
    }
}
