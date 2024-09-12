namespace CNewsProject.Models.Account;

public class UserProfileVM
{
    public AppUser User { get; set; }
    public Subscription? SubInfo { get; set; }
    public List<string> SubscribedTo { get; set; } = new();
}