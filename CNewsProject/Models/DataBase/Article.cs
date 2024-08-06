using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase
{
    public class Article
    {
        public int Id { get; set; }
        public DateTime WrittenDate { get; set; } = DateTime.Now;
        public DateTime PublishedDate { get; set; } = DateTime.Now; 

        [Display(Name = "Link text tooltip helt unneccsessarry")]
        public string LinkText { get; set; } = string.Empty;

        [Required]
        public string Headline { get; set; } = string.Empty;

        [Required]
        public string ContentSummary { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
        public int Views { get; set; }
        public int Likes { get; set; }

        public string ImageLink { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = new();
        public bool IsArchived  { get; set; }
        public string AuthorUserName { get; set; } = string.Empty;
        public string ThePublisherUserName { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }
}
