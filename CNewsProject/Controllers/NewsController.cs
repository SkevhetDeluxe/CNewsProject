using CNewsProject.Models.DataBase;
using CNewsProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
	public class NewsController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly ICategoryService _categoryService;        
        private readonly IWeatherService _weatherService;
        private readonly IExchangeRateService _exchangeRateService;

        public NewsController(IArticleService articleService, ICategoryService categoryService, IWeatherService weatherService, IExchangeRateService exchangeRateService)
		{
			_articleService = articleService;
			_categoryService = categoryService;
			_weatherService = weatherService;
			_exchangeRateService = exchangeRateService;
		}
		public IActionResult Local()
		{
			Category category = _categoryService.GetCategoryById(1); // CHANGE TO THE CORRECT NUMBER LATER WHEN WE HAVE OUR DB
			List<Article> localArticles = _articleService.GetArticleListByCategory(category);
			return View(localArticles);
		}

		public IActionResult Sweden()
		{
			Category category = _categoryService.GetCategoryById(1); // CHANGE TO THE CORRECT NUMBER LATER WHEN WE HAVE OUR DB
			List<Article> swedenArticles = _articleService.GetArticleListByCategory(category);
			return View(swedenArticles);
		}

		public IActionResult World()
		{
			Category category = _categoryService.GetCategoryById(1); // CHANGE TO THE CORRECT NUMBER LATER WHEN WE HAVE OUR DB
			List<Article> worldArticles = _articleService.GetArticleListByCategory(category);
			return View(worldArticles);
		}

		public IActionResult Economy()
		{
			Category category = _categoryService.GetCategoryById(1); // CHANGE TO THE CORRECT NUMBER LATER WHEN WE HAVE OUR DB
			List<Article> economyArticles = _articleService.GetArticleListByCategory(category);
			return View(economyArticles);
		}

		public IActionResult Sport()
		{
			Category category = _categoryService.GetCategoryById(1); // CHANGE TO THE CORRECT NUMBER LATER WHEN WE HAVE OUR DB
			List<Article> sportArticles = _articleService.GetArticleListByCategory(category);
			return View(sportArticles);
		}

		public IActionResult Search()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Search(string search)
		{
			List<Article> searchResults = _articleService.SearchForArticles(search);
			return View(searchResults);
		}
	}
}
