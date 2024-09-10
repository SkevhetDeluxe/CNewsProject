
namespace CNewsProject.Models.DataBase.Identity
{
    public class AppUser : IdentityUser
    {
        public List<int> LikedArticles { get; set; } = new();
        public bool Fire { get; set; } // Deprecated. Might cause error if removed.
        
        public DateTime TimeCreateCustomer { get; set; } = DateTime.Now;
        
        
        #region NewsLetterSettings
        public List<int> CategoryIds { get; set; } = new() {1,2,3,4,5,};
        public List<string>? AuthorNames { get; set; }
        public bool Latest { get; set; } = true;
        public bool Popular { get; set; } = true;
        #endregion
    }
}
