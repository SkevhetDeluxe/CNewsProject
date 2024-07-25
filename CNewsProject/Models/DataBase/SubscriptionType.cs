using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase
{
    public class SuscriptionType
    {
        public int Id { get; set; }

        [Required]
        public string TypeName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
