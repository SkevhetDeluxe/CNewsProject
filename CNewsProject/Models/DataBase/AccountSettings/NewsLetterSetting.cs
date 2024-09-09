namespace CNewsProject.Models.DataBase.AccountSettings;

public class NewsLetterSetting
{
    public NewsLetterSetting()
    {
        
    }

    public NewsLetterSetting(AppUser user)
    {
        User = user;
        UserId = user.Id;
    }
    
    public int Id { get; set; }
    public AppUser? User { get; set; }
    public string UserId { get; set; } = "NoID";
    public IList<int> CategoryIds { get; set; } = new List<int>();
    public IList<string> AuthorNames { get; set; } = new List<string>();
    public bool Latest { get; set; } = true;
    public bool Popular { get; set; } = true;
}