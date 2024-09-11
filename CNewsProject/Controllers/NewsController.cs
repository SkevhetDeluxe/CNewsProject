using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CNewsProject.Controllers
{
	public class NewsController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly ICategoryService _categoryService;
        private readonly IVisitorCountService _visitorCountService;
		private readonly IIdentityService _identityService;
        private readonly ISubscriptionService _subscriptionService;		
		private readonly ApplicationDbContext _context;

		public NewsController(IArticleService articleService, ICategoryService categoryService,
			IVisitorCountService visitorCountService, IIdentityService iService, ISubscriptionService subService, ApplicationDbContext context)
		{
			_subscriptionService = subService;
            _articleService = articleService;
			_categoryService = categoryService;
			_visitorCountService = visitorCountService;
			_identityService = iService;            
			_context = context;
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
        public async Task<IActionResult> Index() //This action AVG at 6000 ms. It should NOT go above 500 ms. We need to take a look at this.
        {
	        // I don't know if this is legal.
	        if (StaticTempData.AuthorNames.Initialised == false)
	        {
		        List<string> authorNames = await _articleService.GetAllAuthorNames();
		        StaticTempData.AuthorNames.UserNames = authorNames;
		        StaticTempData.AuthorNames.Initialised = true;
	        }
	        
	        
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

		public IActionResult Search() // REWORKING SEARCH
		{
			return View();
		}
		[HttpPost]
		public IActionResult Search(string search, string category)
		{
			SearchResult result = _articleService.SearchForArticles(search, category);
			return View(result.Articles);
		}
		
		[Authorize]
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

        public IActionResult Archive()
        {
            var articles = _context.Article
            .FromSqlRaw("SELECT * FROM Article WHERE IsArchived = 1")
			.ToList();
            
            // This DataType is not MATCHING the @model; WILL give EXCEPTION
            return View(articles);
        }

        public JsonResult SearchAuthors(string term)
        {
            var authors = _context.Article
                .Where(a => a.AuthorUserName.Contains(term))
                .Select(a => a.AuthorUserName)
                .Distinct()
                .Take(10)
                .ToList();

            return Json(authors);
        }

        // SHould be done in Azure Funciton. NOT HERE!
        // public IActionResult ArchiveOldArticles()
        // {
        // 	var archiveDate = DateTime.Now.AddMonths(-6); // Archive articles older than 6 months
        //
        // 	var oldArticles = _context.Article
        // 		.Where(a => a.PublishedDate < archiveDate && !a.IsArchived);
        //
        // 	foreach (var article in oldArticles)
        // 	{
        // 		article.IsArchived = true;
        // 	}
        //
        // 	_context.SaveChanges();
        //
        // 	return RedirectToAction("Archive");
        // }

        //public async Task TranslateDocuments()
        //{
        //    var translationService = new DocumentTranslationService();

        //    string inputContainerUri = "https://<your-input-container-url>";
        //    string outputContainerUri = "https://<your-output-container-url>";
        //    string targetLanguage = "es";

        //    await translationService.TranslateDocumentAsync(inputContainerUri, outputContainerUri, targetLanguage);
        //}
    }

}
