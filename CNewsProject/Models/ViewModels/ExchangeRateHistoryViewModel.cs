namespace CNewsProject.Models.ViewModels
{
    public class ExchangeRateHistoryViewModel
    {
        public DateTime Date { get; set; }
        public decimal USD { get; set; }
        public decimal EUR { get; set; }
        public decimal GBP { get; set; }
    }
}
