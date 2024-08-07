
namespace CNewsProject.Controllers
{
	public class NewsController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly ICategoryService _categoryService;
        private readonly IVisitorCountService _visitorCountService;

        public NewsController(IArticleService articleService, ICategoryService categoryService, IVisitorCountService visitorCountService)
		{
			_articleService = articleService;
			_categoryService = categoryService;
			_visitorCountService = visitorCountService;
		}

        public IActionResult Index()
        {
            FrontPageArticlesVM vModel = _articleService.GetFrontPageArticleVM();
			return View(vModel);
        }
        public IActionResult Local()
		{
			//Category category = _categoryService.GetCategoryById(1); // CHANGE TO THE CORRECT NUMBER LATER WHEN WE HAVE OUR DB
			//List<Article> localArticles = _articleService.GetArticleListByCategory(category);

			// Hello Guys it's me. I stringified it!
			return View(_articleService.GetArticleListByCategoryStringified("Local")); 
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
		public IActionResult Search(string search, string category)
		{
			List<Article> searchResults = _articleService.SearchForArticles(search, category);
			return View(searchResults);
		}
		public IActionResult Article(int id)
		{
			UserAndArticleIdCarrier vModel = new() { ArticleId = id, Principal = User };

			return View(vModel);
		}
	}
}
