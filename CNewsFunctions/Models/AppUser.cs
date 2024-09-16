
using Microsoft.AspNetCore.Identity;

namespace CNewsFunctions.Models
{
    public class AppUser : IdentityUser
    {
        // Credentials
        public string UserName { get; set; } = "INIT";
        public string Email { get; set; } = "INIT";

        #region NewsLetterSettings
        public bool NewsLetterEnabled { get; set; } = true;
        public List<int> CategoryIds { get; set; } = new() {1,2,3,4,5,};
        public List<string>? AuthorNames { get; set; }
        public bool Latest { get; set; } = true;
        public bool Popular { get; set; } = true;
        #endregion
    }
}
