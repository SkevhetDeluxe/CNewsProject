
namespace CNewsProject.Controllers
{
    [Authorize(Roles = "The Publisher, Admin")]
    public class ThePublisherController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IIdentityService _identityService;

        public ThePublisherController(IArticleService articleService, IIdentityService identitySrvc
            ,ICategoryService categoryService)
        {
            _articleService = articleService;
            _identityService = identitySrvc;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            List<Article> model = _articleService.GetPendingArticles();
            return View(model);
        }

        public IActionResult Review(int id)
        {
            //string category = article.Category.Name;
            Article article = _articleService.GetArticleById(id);

            ReviewArticleVM vModel = new()
            {
                Article = article,
                ArticleId = id,
                CategoryName = article.Category.Name
            };

            return View(vModel);
        }


        [HttpPost]
        public IActionResult Preview(ReviewArticleVM vModel, string content)
        {
            Article model = _articleService.GetArticleById(vModel.ArticleId);
            model.Headline = vModel.Article.Headline;
            model.Content = content;
            model.ContentSummary = vModel.Article.ContentSummary;
            model.Category = _categoryService.GetCategoryByName(vModel.CategoryName);

            _articleService.UpdateArticle(model);

            return View(model);
        }

        public async Task<IActionResult> Publish(int id)
        {
            var publisher = await _identityService.GetAppUserByClaimsPrincipal(User);

            if (publisher != null)
            {
                _articleService.PublishArticle(id, publisher.UserName!);
                return View(_articleService.GetArticleById(id)!);
            }

            return RedirectToAction("Oops");
        }

        public IActionResult Decline(int id)
        {
            DeclineVM vModel = new()
            {
                HeadLine = _articleService.GetArticleById(id).Headline,
                Id = id
            };

            return View(vModel);
        }

        [HttpPost]
        public async Task<IActionResult> Decline(DeclineVM vModel)
        {
            _articleService.DeclineArticle(vModel.Id, vModel.Reason);
            return View("/Views/ThePublisher/SuccessfullyDeclined.cshtml", vModel);
        }

        public IActionResult TakeDown(int id) // TODO
        {
            var article = _articleService.GetArticleById(id);

            return View(article);
        }

        [HttpPost]
        public IActionResult TakeDown(Article article)
        {
            return View(article);
        }

        public IActionResult Oops()
        {
            return View();
        }
    }
}
