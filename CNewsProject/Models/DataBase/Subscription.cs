using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase
{
    public class Subscription
    {
        public int Id { get; set; }

        [Required]
        public string SubscriptionType { get; set; } = string.Empty;

        public decimal HistoricalPrice { get; set; }

        [Required]
        public date CreateDate { get; set; }

        [Required]
        public date ExpiresDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool PaymentComplete { get; set; }

    }
}