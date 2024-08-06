
namespace CNewsProject.Controllers
{
	public class NewsController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly ICategoryService _categoryService;

		public NewsController(IArticleService articleService, ICategoryService categoryService)
		{
			_articleService = articleService;
			_categoryService = categoryService;
		}
		public IActionResult Local()
		{
			// Hello Guys it's me. I stringified it!
			return View(_articleService.GetArticleListByCategoryStringified("Local")); 
		}

		public IActionResult Sweden()
		{
			return View(_articleService.GetArticleListByCategoryStringified("Sweden"));
		}

		public IActionResult World()
		{
			return View(_articleService.GetArticleListByCategoryStringified("World"));
		}

		public IActionResult Economy()
		{
			return View(_articleService.GetArticleListByCategoryStringified("Economy"));
		}

		public IActionResult Sport()
		{
			return View(_articleService.GetArticleListByCategoryStringified("Sport"));
		}

		public IActionResult Search()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Search(string search, string category)
		{
			List<Article> searchResults = _articleService.SearchForArticles(search, category);
			return View(searchResults);
		}
		public IActionResult Article(int id)
		{
			Article article = _articleService.GetArticleById(id);
			return View(article);
		}
	}
}
