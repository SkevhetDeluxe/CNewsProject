namespace CNewsProject.Models.Api.CurrencyExchangeRate
{
    public class CurrencyExchangeRate
    {
        public List<string>? Country { get; set; }
        public List<decimal>? Valuta { get; set; }
    }
               
    public class CurrencyExchangeRateStats
    {
        public string? Country { get; set; }
        public decimal? Valuta { get; set; }
            
    }
    


}
