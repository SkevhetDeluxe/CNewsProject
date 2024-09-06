using CNewsProject.Models.DataBase;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.ViewModels
{
    public class ArticleListVM
    {
    
        public List<Article> ArticleList { get; set; } = new List<Article>();

        public string Headline { get; set; } = "All";
        public string Category { get; set; } = "All";
        public List<SelectListItem> CategoryList { get; set; } = new();
        public List<SelectListItem> HeadlineList { get; set; } = new();

        public List<SelectListItem> PageSizeList { get; set; } = new()
        {
            { new SelectListItem () { Text = "5", Value = "5" } },
            { new SelectListItem () { Text = "10", Value = "10" } },
            { new SelectListItem () { Text = "15", Value = "15" } },
            { new SelectListItem () { Text = "20", Value = "20" } },
            { new SelectListItem () { Text = "25", Value = "25" } }
        };

        [Display(Name = "Page Size")]
        public string SelectedPageSize { get; set; } = "20";

        public int PageNum { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int MaxPage { get; set; }
    }
}
