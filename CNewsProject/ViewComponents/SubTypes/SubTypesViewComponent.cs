namespace CNewsProject.ViewComponents.SubTypes
{
    public class SubTypesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<SubscriptionType> subTypes)
        {
            return View(subTypes);
        }
    }
}
