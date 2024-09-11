namespace CNewsProject.Controllers
{
    [Authorize(Roles = "Journalist")]
    public class JournalistController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IIdentityService _identityService;

        public JournalistController(IArticleService articleService, IIdentityService identitySrvc)
        {
            _articleService = articleService;
            _identityService = identitySrvc;
        }

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
                AppUser author = await _identityService.GetAppUserByClaimsPrincipal(User);

                //_articleService.WriteArticle(vModel, content, author.UserName!, draft);

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
                var author = await _identityService.GetAppUserByClaimsPrincipal(User);

                var article = _articleService.GetArticleById(id);

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
            var author = await _identityService.GetAppUserByClaimsPrincipal(User);
            
            var article = _articleService.GetArticleById(vModel.ArticleId);
            
            if(article.AuthorUserName != author.UserName!)
                return RedirectToAction("Denied", "Account");
            
            if (ModelState.IsValid)
            {
                //var result = _articleService.UpdateArticleFromEditVM(vModel, draft);
                
                //if(result)
                //    return RedirectToAction("Index");
                
                return View(vModel);
            }
            else
            {
                return View(vModel);
            }
        }
    }
}