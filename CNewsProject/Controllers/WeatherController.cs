using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using CNewsProject.Models.ViewModels; // Create a view model to handle weather data
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CNewsProject.Controllers
{
	public class WeatherController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
        private readonly TableClient _tableClient;

        

        public async Task<IActionResult> WeatherHistory()
        {
            var weatherHistory = new List<WeatherForArchiveViewModel>();

            await foreach (var entity in _tableClient.QueryAsync<WeatherForArchive>())
            {
                weatherHistory.Add(new WeatherForArchiveViewModel
                {
                    Date = entity.DateUpdated,
                    Temperature = entity.Temperature,
                    Condition = entity.Condition
                });
            }

            return View(weatherHistory);
        }

        public WeatherController(ApplicationDbContext dbContext, TableServiceClient tableServiceClient)
		{
			_dbContext = dbContext;

            _tableClient = tableServiceClient.GetTableClient("WeatherArchive");
        }

		public IActionResult HistoricalData()
		{
			var historicalData = _dbContext.HistoricalWeathers
				.OrderByDescending(h => h.Date)
				.ToList();

			return View(historicalData);
		}
	}
}
