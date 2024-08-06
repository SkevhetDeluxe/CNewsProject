namespace CNewsProject.Models.ViewModels
{
    public class UserAndArticleIdCarrier
    {
        public System.Security.Claims.ClaimsPrincipal? Principal { get; set; }
        public int ArticleId { get; set; }
    }
}
