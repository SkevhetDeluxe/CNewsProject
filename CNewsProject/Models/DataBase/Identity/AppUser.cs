using CNewsProject.Models.DataBase.AccountSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CNewsProject.Models.DataBase.Identity
{
    public class AppUser : IdentityUser
    {
        public List<int> LikedArticles { get; set; } = new();
        public bool Fire { get; set; }


        public int NewsLetterSettingId { get; set; } = 2;
        public NewsLetterSetting? NewsLetterSetting { get; set; } = new();
        public DateTime TimeCreateCustomer { get; set; } = DateTime.Now;

    }
}
