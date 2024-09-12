using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;

namespace CNewsProject.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;
        public SubscriptionController(ISubscriptionService subscriptionService, IIdentityService filipService, IConfiguration configuration)
        {
            _subscriptionService = subscriptionService;
            _identityService = filipService;
			_configuration = configuration;
		}

        public ViewResult Index() => View(_subscriptionService.GetAllTypes());

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
            var user = _identityService.GetAppUserByClaimsPrincipal(User);
            
            var subscription = _subscriptionService.GetSubscriptionById(subscriptionId);

            //subscription.UserId = userId;
            //subscription.CreateDate = DateOnly.FromDateTime(DateTime.Now);
            subscription.ExpiresDate = DateTime.Now.AddMonths(1); //  1 month subscription
            subscription.PaymentComplete = true; // payment is done

            _subscriptionService.AddSubscription(subscription);

            return RedirectToAction("Details", "News");
        }
        // Bayad cod ienja dayel shawad sh. Si?
   
        public IActionResult Index2()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult BuySubscription(string membershipType, string paymentFrequency)
        {

            long narmalPris = 29;

            if (membershipType == "middle")
                narmalPris = 59;
            else if (membershipType == "expensive")
                narmalPris = 189;

            if (paymentFrequency == "yearly")
            {
                narmalPris = narmalPris * 11;
                membershipType += " yearly";
            }
            else
            {
                membershipType += " monthly";
            }




            // Set Stripe secret key
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            // Create options for the Stripe session
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = narmalPris * 100, // Amount in cents
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = membershipType,
                            },
                        },
                        Quantity = 1,
                    }
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:7093/subscription/success",
                CancelUrl = "https://localhost:7093/subscription/cancel"
            };

            // Create session service and session object
            var service = new SessionService();
            Session session = service.Create(options);

            // Redirect to Stripe checkout session URL
            return Redirect(session.Url);
        }

        // Success action
        public IActionResult Success()
        {
            return View();
        }

        // Cancel action
        public IActionResult Cancel()
        {
            return View();
        }

        public IActionResult ThankYou()
		{
			return View();
		}
	}
}
