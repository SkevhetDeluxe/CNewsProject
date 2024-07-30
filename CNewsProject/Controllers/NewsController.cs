using CNewsProject.Models.DataBase;
using CNewsProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
	public class NewsController : Controller
	{
		// DONT KNOW WHY THIS DOESNT WORK TBH
		// PLEASE LET ME KNOW

		//private readonly IArticleService _articleService;
		//private readonly ICategoryService _categoryService;

		public NewsController(/*IArticleService articleService, ICategoryService categoryService*/)
		{
			//_articleService = articleService;
			//_categoryService = categoryService;
		}
		public IActionResult Local()
		{
			//Category category = _categoryService.GetCategoryByName("Local"); // Maybe change this to ID later when we have database ready
			//List<Article> localArticles = _articleService.GetArticleListByCategory(category);
			return View(/*localArticles*/);
		}

		public IActionResult Sweden()
		{
			//Category category = _categoryService.GetCategoryByName("Sweden"); // Maybe change this to ID later when we have database ready
			//List<Article> swedenArticles = _articleService.GetArticleListByCategory(category);
			return View(/*swedenArticles*/);
		}

		public IActionResult World()
		{
			//Category category = _categoryService.GetCategoryByName("World"); // Maybe change this to ID later when we have database ready
			//List<Article> worldArticles = _articleService.GetArticleListByCategory(category);
			return View(/*worldArticles*/);
		}

		public IActionResult Economy()
		{
			//Category category = _categoryService.GetCategoryByName("Economy"); // Maybe change this to ID later when we have database ready
			//List<Article> economyArticles = _articleService.GetArticleListByCategory(category);
			return View(/*economyArticles*/);
		}

		public IActionResult Sport()
		{
			//Category category = _categoryService.GetCategoryByName("Sport"); // Maybe change this to ID later when we have database ready
			//List<Article> sportArticles = _articleService.GetArticleListByCategory(category);
			return View(/*sportArticles*/);
		}
	}
}
