namespace CNewsProject.Models.ViewModels
{
    public class ArticleLock
    {
        public Article Article { get; set; } = new();
        public string Access { get; set; } = "ObjectionMyLord";
    }
}
