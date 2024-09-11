namespace CNewsProject.ViewComponents.Account;

public class NewsLetterSettingsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(AppUser user)
    {
        NLUserSetting setting = new NLUserSetting(user);
        var allAuthors = StaticTempData.AuthorNames.UserNames;

        if (user.AuthorNames != null)
        {
            ViewBag.subscribedTo = user.AuthorNames;
            foreach (var sak in user.AuthorNames)
            {
                allAuthors.Remove(sak);
            }
        }
        
        ViewBag.authorList = allAuthors;
        return View(setting);
    }
}