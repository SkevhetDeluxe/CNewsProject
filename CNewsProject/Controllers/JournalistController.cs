
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
        public async Task<IActionResult> CreateArticle(WriteArticleVM vModel)
        {
            if (ModelState.IsValid)
            {
                AppUser author = await _identityService.GetAppUserByClaimsPrincipal(User);
                _articleService.WriteArticle(vModel, author.UserName!);

                return RedirectToAction("Index");
            }
            else
            {
                return View(vModel);
            }
        }
    }
}
