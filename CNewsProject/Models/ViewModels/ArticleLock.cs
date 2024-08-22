namespace CNewsProject.Models.ViewModels
{
    public class ArticleLock
    {
        public Article Article { get; set; } = new();
        public bool Access { get; set; }
        public List<int> UserLikes { get; set; } = new();
    }
}
