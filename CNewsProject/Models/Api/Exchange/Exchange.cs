namespace CNewsProject.Models.Api.Exchange
{
    public class Exchange
    {
        public List<string>? Country { get; set; }
        public List<decimal>? Valuta { get; set; }
    }
               
    public class ExchangeStats
    {
        public string? Country { get; set; }
        public decimal? Valuta { get; set; }
            
    }
    


}
