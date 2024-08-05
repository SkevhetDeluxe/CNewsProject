using CNewsProject.Models.Api.CurrencyExchangeRate;
using CNewsProject.Models.Api.Weather;
using CNewsProject.Models.DataBase;

namespace CNewsProject.Models.ViewModels
{
    public class NewsVM
    {
        public WeatherStats WeatherStats { get; set; } = new();
        public Rates CurrencyExchangeRates { get; set; } = new();
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
