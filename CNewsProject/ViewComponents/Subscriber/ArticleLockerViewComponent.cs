using CNewsProject.Models.Account;
using System.Reflection;
using System.Security.Claims;

namespace CNewsProject.ViewComponents.Subscriber
{
    public class ArticleLockerViewComponent : ViewComponent
    {
        private readonly IIdentityService _identityService;
        private readonly IArticleService _articleService;
        private readonly ApplicationDbContext _db;


        public ArticleLockerViewComponent(IIdentityService saouirhawurhwaouirhawoirjeioa,
            ApplicationDbContext deebee, IArticleService aosijr)
        {
            _identityService = saouirhawurhwaouirhawoirjeioa;
            _articleService = aosijr;
            _db = deebee;
        }

        public IViewComponentResult Invoke(ClaimsPrincipal principal, int id)
        {
            var article = _articleService.GetArticleById(id);
            
            if (principal.Identity.IsAuthenticated)
            {
                AppUser user = _identityService.GetAppUserByClaimsPrincipal(principal).Result;

                if(article.IsntNotFree == true)
                    return View(new ArticleLock()
                    {
                        Article = article,
                        Access = true,
                        UserLikes = user.LikedArticles,
                        Roles = _identityService.GetUsersRolesAsync(user).Result
                    });
                
                bool isSubbed = _identityService.IsSubscribed(user);
                
                if (_identityService.UserHasRole(user, "Admin").Result || isSubbed)
                {
                    return View(new ArticleLock()
                    {
                        Article = article,
                        Access = true,
                        UserLikes = user.LikedArticles,
                        Roles = _identityService.GetUsersRolesAsync(user).Result
                    });
                }
            }

            return View(new ArticleLock() { Article = article, Access = false });
        }
    }
}
