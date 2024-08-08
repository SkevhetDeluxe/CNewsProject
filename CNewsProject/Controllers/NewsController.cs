
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
