using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.ViewModels
{
    public class WriteArticleVM
    {
        public List<SelectListItem> CategoryList { get; set; }
        public string CategoryName { get; set; }

        public WriteArticleVM()
        {
            CategoryName = "Local";

            CategoryList = new()
            {
                { new SelectListItem () { Text = "Local", Value = "Local" } },
                { new SelectListItem () { Text = "Sweden", Value = "Sweden" } },
                { new SelectListItem () { Text = "World", Value = "World" } },
                { new SelectListItem () { Text = "Economy", Value = "Economy" } },
                { new SelectListItem () { Text = "Sport", Value = "Sport" } }
            };
        }


        [Required]
        public string Headline { get; set; } = string.Empty;

        [Required]
        public string ContentSummary { get; set; } = string.Empty;
        
        [Required]
        public IFormFile ArticleImage { get; set; }
    }
}
