namespace CNewsProject.Models.Api.CurrencyExchangeRate
{
	public class ExchangeRateBackgroundService:BackgroundService
	{
		private readonly CurrencyExchangeRateService _currencyExchangeRateService;

		public ExchangeRateBackgroundService(CurrencyExchangeRateService currencyExchangeRateService)
		{
			_currencyExchangeRateService = currencyExchangeRateService;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				await _currencyExchangeRateService.StoreExchangeRatesAsync();
				await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Adjust interval as needed
			}
		}
	}
}
