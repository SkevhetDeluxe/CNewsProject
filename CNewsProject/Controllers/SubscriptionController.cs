using Microsoft.AspNetCore.Mvc;

namespace CNewsProject.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        //public ViewResult Index() => View(_subscriptionService.AvailableTypes());

        public IActionResult Subscribe()
        {
            var model = new SubscriptionViewModel
            {
                AvailableSubscriptions = _subscriptionService.GetAllSubscription()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Subscribe(int subscriptionId)
        {
            Customer customer = new();
            var userId = customer.Id;
            var subscription = _subscriptionService.GetSubscriptionById(subscriptionId);

            //subscription.UserId = userId;
            //subscription.CreateDate = DateOnly.FromDateTime(DateTime.Now);
            subscription.ExpiresDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(1); //  1 month subscription
            subscription.PaymentComplete = true; // payment is done

            _subscriptionService.AddSubscription(subscription);

            return RedirectToAction("Details", "News");
        }
    }
}
