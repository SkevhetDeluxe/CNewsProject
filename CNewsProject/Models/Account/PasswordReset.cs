using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.Account
{
    public class PasswordReset
    {
        public string? Token { get; set; }
        public string? Email { get; set; }


        [Required]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "The passwords do don't not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
