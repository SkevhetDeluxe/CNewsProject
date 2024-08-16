using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase
{
    public class SubscriptionType
    {
        public int Id { get; set; }

        [Required]
        public string TypeName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int AccessLevel { get; set; }

        public SubscriptionType()
        {

        }
        public SubscriptionType(string name, string desc, decimal price)
        {
            TypeName = name; Description = desc; Price = price;
        }
    }
}
