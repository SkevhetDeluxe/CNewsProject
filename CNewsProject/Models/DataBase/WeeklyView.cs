using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase;

public class WeeklyView
{
    public WeeklyView(){}
    public WeeklyView(Article article)
    {
        ArticleId = article.Id;
        Article = article;
    }
    [Key]
    public int Id { get; set; } 

    public int ArticleId { get; set; }
    public Article Article { get; set; }
    
    // NäR? Den här veckan såklaret!
}