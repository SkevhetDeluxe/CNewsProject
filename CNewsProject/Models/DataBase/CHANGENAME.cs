using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase;

public class CHANGENAME
{
    public CHANGENAME(){}
    public CHANGENAME(Article article)
    {
        ArticleId = article.Id;
        Article = article;
    }

    [Key]
    public int ArticleId { get; set; }
    public Article Article { get; set; }
    
    public int Amount { get; set; }
    // NäR? Den här veckan såklaret!
}