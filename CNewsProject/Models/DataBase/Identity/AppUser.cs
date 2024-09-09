using Microsoft.AspNetCore.Identity;

namespace CNewsProject.Models.DataBase.Identity
{
    public class AppUser : IdentityUser
    {
        public List<int> LikedArticles { get; set; } = new();
        public bool Fire { get; set; }
        public DateTime TimeCreateCustomer { get; set; } = DateTime.Now;

    }
}
