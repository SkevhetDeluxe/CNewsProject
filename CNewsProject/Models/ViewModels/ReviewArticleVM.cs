using Microsoft.AspNetCore.Mvc.Rendering;

namespace CNewsProject.Models.ViewModels
{
    public class ReviewArticleVM
    {
        public Article Article { get; set; } = new();
        public List<SelectListItem> CategoryList { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public int ArticleId { get; set; }
        public ReviewArticleVM()
        {
            CategoryList = new()
            {
                { new SelectListItem () { Text = "Local", Value = "Local" } },
                { new SelectListItem () { Text = "Sweden", Value = "Sweden" } },
                { new SelectListItem () { Text = "World", Value = "World" } },
                { new SelectListItem () { Text = "Economy", Value = "Economy" } },
                { new SelectListItem () { Text = "Sport", Value = "Sport" } }
            };
        }
    }
}
