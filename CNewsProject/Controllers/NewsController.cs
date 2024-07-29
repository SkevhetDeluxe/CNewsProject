using CNewsProject.Models.DataBase;
using CNewsProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
	[Route("news")]
	public class NewsController : Controller
	{
		private readonly IArticleService _articleService;

		public NewsController(IArticleService articleService)
		{
			_articleService = articleService;
		}
		public IActionResult Local()
		{
			Category category = new();
			List<Article> localArticles = _articleService.GetArticleListByCategory(category);
			return View(localArticles);
		}

		public IActionResult Sweden()
		{
			Category category = new();
			List<Article> swedenArticles = _articleService.GetArticleListByCategory(category);
			return View(swedenArticles);
		}

		public IActionResult World()
		{
			Category category = new();
			List<Article> worldArticles = _articleService.GetArticleListByCategory(category);
			return View(worldArticles);
		}

		public IActionResult Economy()
		{
			Category category = new();
			List<Article> economyArticles = _articleService.GetArticleListByCategory(category);
			return View(economyArticles);
		}

		public IActionResult Sport()
		{
			Category category = new();
			List<Article> sportArticles = _articleService.GetArticleListByCategory(category);
			return View(sportArticles);
		}
	}
}
