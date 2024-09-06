using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase
{
    public class Subscription
    {
        public int Id { get; set; }

        public int SubscriptionTypeId { get; set; }

        [Required]
        public SubscriptionType SubscriptionType { get; set; } = new();

        public decimal HistoricalPrice { get; set; }

        [Required]
        public DateTime RenewedDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime ExpiresDate { get; set; }

        [Required]
        public string UserId { get; set; } = "No UserId";
        public AppUser User { get; set; } = new();

        [Required]
        public bool PaymentComplete { get; set; }

    }
}