
using CNewsProject.Service;

namespace CNewsProject.Controllers
{
	public class NewsController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly ICategoryService _categoryService;
        private readonly IVisitorCountService _visitorCountService;
		private readonly IIdentityService _identityService;
        private readonly ISubscriptionService _subscriptionService;

        public NewsController(IArticleService articleService, ICategoryService categoryService,
			IVisitorCountService visitorCountService, IIdentityService iService, ISubscriptionService subService)
		{
			_subscriptionService = subService;
            _articleService = articleService;
			_categoryService = categoryService;
			_visitorCountService = visitorCountService;
			_identityService = iService;
            _articleService = articleService;
        }

        // sh
        public IActionResult Details(int id)
        {
            var article = _articleService.GetArticleById(id);
			var userId = _identityService.GetAppUserByClaimsPrincipal(User).Result.Id;

            bool isUserSubscribed = _subscriptionService.IsUserSubscribed(userId);

            if (!isUserSubscribed)
            {
                return RedirectToAction("Subscribe", "Subscription");
            }

            return View(article);
        }
		// sh
        public IActionResult Index() //This action AVG at 6000 ms. It should NOT go above 500 ms. We need to take a look at this.
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
			if (id == 0)
				return RedirectToAction("Missing");

			UserAndArticleIdCarrier vModel = new() { ArticleId = id, Principal = User };
			_articleService.IncreaseViews(id);
			
			return View(vModel);
		}

		public IActionResult Laikalaininen(int articleId)
		{
			_articleService.Laikalaininen(articleId, User);
			return ViewComponent("ArticleLocker", new { principal = User, id = articleId });
		}

		public IActionResult Missing()
		{
			return View();
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
