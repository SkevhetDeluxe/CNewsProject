using Microsoft.Build.Framework;

namespace CNewsProject.Models.Account;

public class NLUserSetting
{
    public NLUserSetting()
    {
        
    }

    public NLUserSetting(AppUser user)
    {
        NewsLetterEnabled = user.NewsLetterEnabled;
        CategoryIds = user.CategoryIds;
        AuthorNames = user.AuthorNames;
        Latest = user.Latest;
        Popular = user.Popular;
    }
    
    #region NewsLetterSettings
    public bool NewsLetterEnabled { get; set; } = true;
    public List<int> CategoryIds { get; set; } = new() {1,2,3,4,5,};
    public List<string>? AuthorNames { get; set; }
    public bool Latest { get; set; } = true;
    public bool Popular { get; set; } = true;
    #endregion
}