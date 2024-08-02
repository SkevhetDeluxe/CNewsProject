using CNewsProject.Models.Api.CurrencyExchangeRate;
using CNewsProject.Models.Api.Weather;
using CNewsProject.Models.DataBase;

namespace CNewsProject.Models.ViewModels
{
    public class NewsVM
    {
        public WeatherStats WeatherStats { get; set; }
        public CurrencyExchangeRate ?CurrencyExchangeRate { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
