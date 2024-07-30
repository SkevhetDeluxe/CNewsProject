using CNewsProject.Models.DataBase.Identity;
using Microsoft.AspNetCore.Identity;

namespace CNewsProject.Models.Roles
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AppUser>? Members { get; set; }
        public IEnumerable<AppUser>? NonMembers { get; set; }
    }
}
