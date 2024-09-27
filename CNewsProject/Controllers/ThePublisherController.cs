
namespace CNewsProject.Controllers
{
    [Authorize(Roles = "The Publisher, Admin")]
    public class ThePublisherController(
        IArticleService articleService,
        IIdentityService identitySrvc,
        ICategoryService categoryService)
        : Controller
    {
        public IActionResult Index()
        {
            List<Article> model = articleService.GetPendingArticles();
            return View(model);
        }

        public IActionResult Review(int id)
        {
            //string category = article.Category.Name;
            Article article = articleService.GetArticleById(id);

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
            Article model = articleService.GetArticleById(vModel.ArticleId);
            model.Headline = vModel.Article.Headline;
            model.Content = content;
            model.ContentSummary = vModel.Article.ContentSummary;
            model.Category = categoryService.GetCategoryByName(vModel.CategoryName);

            articleService.UpdateArticle(model);

            return View(model);
        }

        public async Task<IActionResult> Publish(int id)
        {
            var publisher = await identitySrvc.GetAppUserByClaimsPrincipal(User);

            if (publisher != null)
            {
                articleService.PublishArticle(id, publisher.UserName!);
                return View(articleService.GetArticleById(id)!);
            }

            return RedirectToAction("Oops");
        }

        public IActionResult Decline(int id)
        {
            DeclineVM vModel = new()
            {
                HeadLine = articleService.GetArticleById(id).Headline,
                Id = id
            };

            return View(vModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Decline(DeclineVM vModel)
        {
            articleService.DeclineArticle(vModel.Id, vModel.Reason);
            return View("/Views/ThePublisher/SuccessfullyDeclined.cshtml", vModel);
        }

        [HttpPost]
        public IActionResult TakeDown(int id, string reason)
        {
            articleService.TakeDownArticle(id, reason);
            
            return RedirectToAction("Index", "News");
        }

        public IActionResult Oops()
        {
            return View();
        }
    }
}
