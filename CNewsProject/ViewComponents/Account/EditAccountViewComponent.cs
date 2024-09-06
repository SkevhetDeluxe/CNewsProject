namespace CNewsProject.ViewComponents.Account;

public class EditAccountViewComponent : ViewComponent
{
    private readonly IIdentityService _identityService;
    public EditAccountViewComponent(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public IViewComponentResult Invoke(AppUser user)
    {
        return View(user);
    }
}