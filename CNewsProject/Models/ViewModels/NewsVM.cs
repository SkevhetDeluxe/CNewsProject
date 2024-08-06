using CNewsProject.Models.Api.CurrencyExchangeRate;
using CNewsProject.Models.Api.Weather;
using CNewsProject.Models.DataBase;

namespace CNewsProject.Models.ViewModels
{
    public class NewsVM // This will be deprecated soon. By Me Myself and I!
    {
        public WeatherStats WeatherStats { get; set; } = new(); // <--- Will be made ViewComponents these two ---|
        public Rates CurrencyExchangeRates { get; set; } = new();// <-------------------------------------------/                                         
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
