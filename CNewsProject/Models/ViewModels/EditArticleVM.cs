using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.ViewModels;

public class EditArticleVM
{
    public EditArticleVM()
    {
        CategoryName = "Not Loaded";
        Headline = "Not Loaded";
        ContentSummary = "Not Loaded";
        Content = "Not Loaded";
        BlobLink = "Not Loaded";
        Status = "Not Loaded";
    }
    public EditArticleVM(Article article)
    {
        ArticleId = article.Id;
        CategoryName = article.Category.Name;
        Headline = article.Headline;
        ContentSummary = article.ContentSummary;
        Content = article.Content;
        Status = article.Status;
        DeclineMessage = article.DeclineMessage;
        BlobLink = article.ImageLink;
    }

    public int ArticleId { get; set; }
    
    public List<SelectListItem> CategoryList { get; set; } =
    [
        new SelectListItem() { Text = "Local", Value = "Local" },
        new SelectListItem() { Text = "Sweden", Value = "Sweden" },
        new SelectListItem() { Text = "World", Value = "World" },
        new SelectListItem() { Text = "Economy", Value = "Economy" },
        new SelectListItem() { Text = "Sport", Value = "Sport" }
    ];
    [Required]
    public string CategoryName { get; set; }
    
    [Required] 
    public string Headline { get; set; }

    [Required] 
    public string ContentSummary { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    public IFormFile? ArticleImage { get; set; } = null;
    public string BlobLink { get; set; }

    public string Status { get; set; }
    public string? DeclineMessage { get; set; }
}
