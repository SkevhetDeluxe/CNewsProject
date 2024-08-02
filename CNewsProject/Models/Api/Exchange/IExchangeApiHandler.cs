using CNewsProject.Models.Api.Weather;

namespace CNewsProject.Models.Api.Exchange
{
    public interface IExchangeApiHandler
    {
        public Task<Exchange> GetExchangeAsync(string country);

    }
}
