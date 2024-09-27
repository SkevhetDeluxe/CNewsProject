namespace CNewsProject.Models.ViewModels
{
    public class AuthorArticlesVM
    {
        public List<Article> Approved { get; set; } = new();
        public List<Article> Pending { get; set; } = new();
        public List<Article> Declined { get; set; } = new();
        public List<Article> Draft { get; set; } = new();
        public List<Article> TakenDown { get; set; } = new();
    }
}
