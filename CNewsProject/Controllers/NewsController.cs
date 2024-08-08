
namespace CNewsProject.Controllers
{
	public class NewsController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly ICategoryService _categoryService;
        private readonly IVisitorCountService _visitorCountService;
		private readonly IIdentityService _identityService;

        public NewsController(IArticleService articleService, ICategoryService categoryService,
			IVisitorCountService visitorCountService, IIdentityService iService)
		{
			_articleService = articleService;
			_categoryService = categoryService;
			_visitorCountService = visitorCountService;
			_identityService = iService;
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
			_articleService.IncreaseViews(id);
			
			return View(vModel);
		}

		public IActionResult Laikalaininen(int articleId)
		{
			_articleService.Laikalaininen(articleId, User);
			return ViewComponent("ArticleLocker", new { principal = User, id = articleId });
		}

		[AllowAnonymous]
		[Authorize(Roles = "Admin")]
		public IActionResult SugMinaStats()
		{
			_articleService.GetTheRealStats();

			return RedirectToAction("Index");
		}

	}
}
