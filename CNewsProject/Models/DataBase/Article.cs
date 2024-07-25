using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [DateStamp(Date = "Datestamp")]
        public date DateStamp { get; set; }

        [Required]
        [Display(Name = "Link text")]
        public string LinkText { get; set; } = string.Empty;

        [Required]
        public string HeadLine { get; set; } = string.Empty;

        [Required]
        public string ContentSummary { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public int views { get; set; }

        [Required]
        public int Likes { get; set; }

        [Required]
        public string ImageLink { get; set; } = string.Empty;
       
        [Required]
        public string Categori { get; set; } = string.Empty;

        [Required]
        public bool IsArchived  { get; set; } = string.Empty;


    }
}
