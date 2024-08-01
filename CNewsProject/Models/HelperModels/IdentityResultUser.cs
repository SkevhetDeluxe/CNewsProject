using CNewsProject.Models.DataBase.Identity;
using Microsoft.AspNetCore.Identity;

namespace CNewsProject.Models.HelperModels
{
    public class IdentityResultUser
    {
        public IdentityResult Result { get; set; }
        public AppUser User { get; set; }

        public IdentityResultUser(IdentityResult result, AppUser user)
        {
            Result = result;
            User = user;  
        }
    }
}
