namespace CNewsProject.ViewComponents.News
{
    public class SidebarHeadlinesViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;

        public SidebarHeadlinesViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke(int count)
        {
            List<Article> approvedArticles = _articleService.GetAllPublished();

            SidebarHeadlinesVM vModel = new()
            {
                localArticles = approvedArticles.Where(a => a.Category.Name == "Local")
                    .OrderByDescending(a => a.PublishedDate).Take(count)
                    .ToList(),
                swedenArticles = approvedArticles.Where(a => a.Category.Name == "Sweden")
                    .OrderByDescending(a => a.PublishedDate).Take(count)
                    .ToList(),
                worldArticles = approvedArticles.Where(a => a.Category.Name == "World")
                    .OrderByDescending(a => a.PublishedDate).Take(count)
                    .ToList(),
                economyArticles = approvedArticles.Where(a => a.Category.Name == "Economy")
                    .OrderByDescending(a => a.PublishedDate).Take(count)
                    .ToList(),
                sportArticles = approvedArticles.Where(a => a.Category.Name == "Sport")
                    .OrderByDescending(a => a.PublishedDate).Take(count)
                    .ToList()
            };
            return View(vModel);
        }
    }
}
