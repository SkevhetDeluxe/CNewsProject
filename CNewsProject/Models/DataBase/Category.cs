using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string CategoryType { get; set; } = string.Empty;
    }
}