
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
			CategoryPageArticlesVM vModel = _articleService.GetCategoryPageArticleVM("Local");
			return View(vModel);
		}

		public IActionResult Sweden()
		{
			CategoryPageArticlesVM vModel = _articleService.GetCategoryPageArticleVM("Sweden");
			return View(vModel);
		}

		public IActionResult World()
		{
			CategoryPageArticlesVM vModel = _articleService.GetCategoryPageArticleVM("World");
			return View(vModel);
		}

		public IActionResult Economy()
		{
			CategoryPageArticlesVM vModel = _articleService.GetCategoryPageArticleVM("Economy");
			return View(vModel);
		}

		public IActionResult Sport()
		{
			CategoryPageArticlesVM vModel = _articleService.GetCategoryPageArticleVM("Sport");
			return View(vModel);
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

        [HttpPost("IncreaseViews/{id}")]
        public IActionResult IncreaseViews(int id)
        {
            _articleService.IncreaseViews(id);
            return Ok();
        }

        [HttpPost("IncreaseLikes/{id}")]
        public IActionResult IncreaseLikes(int id)
        {
            _articleService.IncreaseLikes(id);
            return Ok();
        }
    }
}
