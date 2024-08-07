namespace CNewsProject.Models.DataBase
{
    public class EditorsChoice
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; } = new();
    }
}
