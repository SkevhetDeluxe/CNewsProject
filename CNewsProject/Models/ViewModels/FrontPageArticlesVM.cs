namespace CNewsProject.Models.ViewModels
{
    public class FrontPageArticlesVM
    {
        public Article MainArticle { get; set; } = new();
        public List<Article> NotMainButStillImportantArticles { get; set; } = new();
        public List<Article> TheRestLol { get; set; } = new();
    }
}
