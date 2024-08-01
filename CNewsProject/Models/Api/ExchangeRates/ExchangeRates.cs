using System.Collections.Generic;

namespace CNewsProject.Models.Api.ExchangeRates
{
    public class ExchangeRate
    {
        public string Currency { get; set; }
        public double Rate { get; set; }
    }

    public class ExchangeRates
    {
        public List<ExchangeRate> Rates { get; set; }
    }
}
