using System.ComponentModel.DataAnnotations;

namespace CNewsProject.Models.Account
{
    public class Login
    {
        [Required]
        public string EmailUsername { get; set; }

        [Required]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
