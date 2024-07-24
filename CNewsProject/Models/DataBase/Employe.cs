using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.DataBase
{
    public class Employe
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)] //Max (value) amount of chars in string
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(75)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(75)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(6)]
        public string Zip { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = string.Empty;

        [StringLength(15)]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        
        public int EmployeAsId { get; set; }

    }
}
