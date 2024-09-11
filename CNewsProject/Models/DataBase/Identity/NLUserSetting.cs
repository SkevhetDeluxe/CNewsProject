namespace CNewsProject.Models.DataBase.Identity;

public class NLUserSetting // Just for fetching
{
    public NLUserSetting() { }

    public NLUserSetting(AppUser user)
    {
        CategoryIds = user.CategoryIds;
        AuthorNames = user.AuthorNames;
        Latest = user.Latest;
        Popular = user.Popular;
    }
    
    public List<int> CategoryIds { get; set; } = new List<int>() {1,2,3,4,5};
    public List<string>? AuthorNames { get; set; }
    public bool Latest { get; set; } = true;
    public bool Popular { get; set; } = true;
}