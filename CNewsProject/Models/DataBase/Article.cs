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
        [MaxLength(150)]
        public string Headline { get; set; } = string.Empty;

        [Required]
        public string ContentSummary { get; set; } = string.Empty;

        [Required]
        [MaxLength(7000)]
        public string Content { get; set; } = string.Empty;
        public int Views { get; set; }
        public ICollection<WeeklyView>? WeeklyViews { get; set; }
        public int Likes { get; set; }

        public string ImageLink { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = new();
        public string AuthorUserName { get; set; } = string.Empty;
        public string ThePublisherUserName { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";

        public string? DeclineMessage { get; set; } = null;

        // I don't know what this says actually but the -->
        // intended meaning is that false means you have to pay for this. // CORRECT
        public bool IsntNotFree { get; set; } = false; // false = paid for. I think??? // INCORRECT
    }
}
