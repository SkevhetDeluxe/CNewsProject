using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase;

public class WeeklyView(Article article)
{
    [Key]
    public int Id { get; set; } 

    public int ArticleId { get; set; } = article.Id;
    public Article Article { get; set; } = article;
    
    // NäR? Den här veckan såklaret!
}