
namespace CNewsProject.Controllers
{
    [Authorize(Roles = "Journalist")]
    public class JournalistController : Controller
    {
        private readonly IArticleService _articleService;

        public JournalistController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult CreateArticle() => View(new WriteArticleVM());

        [HttpPost]
        public IActionResult CreateArticle(WriteArticleVM vModel)
        {
            if (ModelState.IsValid)
            {
                _articleService.WriteArticle(vModel);

                return RedirectToAction("Index");
            }
            else
            {
                return View(vModel);
            }
        }
    }
}
