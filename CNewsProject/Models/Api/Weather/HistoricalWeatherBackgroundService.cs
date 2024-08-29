namespace CNewsProject.Models.Api.Weather
{
	public class HistoricalWeatherBackgroundService:BackgroundService
	{
		private readonly WeatherApiHandler _weatherApiHandler;

		public HistoricalWeatherBackgroundService(WeatherApiHandler weatherApiHandler)
		{
			_weatherApiHandler = weatherApiHandler;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				DateTime date = DateTime.Now.AddDays(-1); // Get yesterday's weather data
				await _weatherApiHandler.FetchAndStoreHistoricalWeatherAsync("Stockholm", date);

				await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Run every 24 hours
			}
		}
	}
}
