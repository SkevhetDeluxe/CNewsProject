using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase
{
    public class VisitorCount
    {
        public int Id { get; set; }

        [Required]
        public string PageName { get; set; }

        [Required]
        public int VisitCount { get; set; }
    }
}
