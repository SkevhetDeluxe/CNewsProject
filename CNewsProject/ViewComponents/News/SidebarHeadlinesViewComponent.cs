namespace CNewsProject.ViewComponents.News
{
    public class SidebarHeadlinesViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;

        public SidebarHeadlinesViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            SidebarHeadlinesVM vModel = new();

            vModel.localArticles = _articleService.GetArticleListByCategoryStringified("Local", 5);
            vModel.swedenArticles = _articleService.GetArticleListByCategoryStringified("Sweden", 5);
            vModel.worldArticles = _articleService.GetArticleListByCategoryStringified("World", 5);
            vModel.economyArticles = _articleService.GetArticleListByCategoryStringified("Economy", 5);
            vModel.sportArticles = _articleService.GetArticleListByCategoryStringified("Sport", 5);

            return View(vModel);
        }
    }
}
