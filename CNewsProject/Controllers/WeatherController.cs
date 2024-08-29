using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
	public class WeatherController : Controller
	{
		private readonly ApplicationDbContext _dbContext;

		public WeatherController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
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
