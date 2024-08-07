using System.Security.Claims;

namespace CNewsProject.ViewComponents.Subscriber
{
    public class ArticleLockerViewComponent : ViewComponent
    {
        private readonly IIdentityService _identityService;
        private readonly IArticleService _articleService;
        private readonly ApplicationDbContext _db;

        public ArticleLockerViewComponent(IIdentityService saouirhawurhwaouirhawoirjeioa, ApplicationDbContext deebee, IArticleService aosijr)
        {
            _identityService = saouirhawurhwaouirhawoirjeioa;
            _articleService = aosijr;
            _db = deebee;
        }

        public IViewComponentResult Invoke(ClaimsPrincipal principal, int id)
        {
            if (principal.Identity.IsAuthenticated == true)
            {
                AppUser user = _identityService.GetAppUserByClaimsPrincipal(principal).Result;

                if (_identityService.UserHasRole(user, "Subscriber").Result)
                {
                    return View(new ArticleLock() { Article = _articleService.GetArticleById(id), Access = "AccessGranted" });
                }
                else
                    return View(new ArticleLock() { Article = _articleService.GetArticleById(id), Access = "ObjectionMyLord" });
            }
            else
            {
                return View(new ArticleLock() { Article = _articleService.GetArticleById(id), Access = "ThisUserHasntFuckingPaidForTheSubscriptionServiceSoHeWillNotGetTheNewArticles" });
            }
        }
    }
}
