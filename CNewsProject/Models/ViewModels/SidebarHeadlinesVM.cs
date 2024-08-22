namespace CNewsProject.Models.ViewModels
{
    public class SidebarHeadlinesVM
    {
        public List<Article> localArticles = new();
        public List<Article> swedenArticles = new();
        public List<Article> worldArticles = new();
        public List<Article> economyArticles = new();
        public List<Article> sportArticles = new();
    }
}
