using CNewsProject.Data;
using System.Security.Claims;

namespace CNewsProject.ViewComponents.Journalist
{
    public class JournalistArticlesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        private readonly IIdentityService _identityService;

        public JournalistArticlesViewComponent(ApplicationDbContext db, IIdentityService identityService)
        {
            _db = db;
            _identityService = identityService;
        }

        public IViewComponentResult Invoke(ClaimsPrincipal principal)
        {
            AppUser user = _identityService.GetAppUserByClaimsPrincipal(principal).Result;

            if (user == null)
                return View(new AuthorArticlesVM());

            string authorUserName = user.UserName!;

            List<Article> articleList = _db.Article.Where(a => a.AuthorUserName == authorUserName).ToList();

            AuthorArticlesVM vModel = new()
            {
                Approved = articleList.Where(a => a.Status == "Approved").ToList() ?? new(),
                Pending = articleList.Where(a => a.Status == "Pending").ToList() ?? new(),
                Declined = articleList.Where(a => a.Status == "Declined").ToList() ?? new(),
                Draft = articleList.Where(a => a.Status == "Draft").ToList() ?? new(),
            };

            return View(vModel);
        }
    }
}
