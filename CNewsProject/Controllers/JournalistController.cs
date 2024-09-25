namespace CNewsProject.Controllers
{
    [Authorize(Roles = "Journalist")]
    public class JournalistController(IArticleService articleService, IIdentityService identitySrvc)
        : Controller
    {
        public IActionResult Index()
        {
            return View(User);
        }

        public ViewResult CreateArticle() => View(new WriteArticleVM());

        [HttpPost]
        public async Task<IActionResult> CreateArticle(WriteArticleVM vModel, string content, bool draft)
        {
            if (ModelState.IsValid)
            {
                var author = await identitySrvc.GetAppUserByClaimsPrincipal(User);

                articleService.WriteArticle(vModel, content, author.UserName!, draft);

                return RedirectToAction("Index");
            }
            else
            {
                return View(vModel);
            }
        }

        public async Task<IActionResult> EditArticle(int id)
        {
            try
            {
                var author = await identitySrvc.GetAppUserByClaimsPrincipal(User);

                var article = articleService.GetArticleById(id);

                if (article.AuthorUserName == author.UserName!)
                {
                    return View(new EditArticleVM(article));
                }
                return RedirectToAction("Denied", "Account");
            }
            catch
            {
                return RedirectToAction("Denied", "Account");
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditArticle(EditArticleVM vModel, bool draft)
        {
            var author = await identitySrvc.GetAppUserByClaimsPrincipal(User);
            
            var article = articleService.GetArticleById(vModel.ArticleId);
            
            if(article.AuthorUserName != author.UserName!)
                return RedirectToAction("Denied", "Account");
            
            if (ModelState.IsValid)
            {
                var result = articleService.UpdateArticleFromEditVM(vModel, draft);
                
                if(result)
                    return RedirectToAction("Index");
            }

            return View(vModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var user = await identitySrvc.GetAppUserByClaimsPrincipal(User);
            var result = articleService.JournalistRemoveArticle(id, user);
            
            return !result ? RedirectToAction("Denied", "Account") : RedirectToAction("Index");
        }
    }
}