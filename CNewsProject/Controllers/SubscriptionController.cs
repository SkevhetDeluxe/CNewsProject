using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using static CNewsProject.StaticTempData.SubscriptionStripeTokens;

namespace CNewsProject.Controllers
{
    [Authorize]
    public class SubscriptionController(
        ISubscriptionService subscriptionService,
        IIdentityService filipService,
        IConfiguration configuration)
        : Controller
    {
        public ViewResult Index() => View(subscriptionService.GetAllTypes());

        [HttpGet]
        public IActionResult Subscribe()
        {
            var model = new SubscriptionViewModel
            {
                AvailableSubscriptions = subscriptionService.GetAllSubscription()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Subscribe(int subscriptionId)
        {
            var user = filipService.GetAppUserByClaimsPrincipal(User);
            
            var subscription = subscriptionService.GetSubscriptionById(subscriptionId);

            //subscription.UserId = userId;
            //subscription.CreateDate = DateOnly.FromDateTime(DateTime.Now);
            subscription.ExpiresDate = DateTime.Now.AddMonths(1); //  1 month subscription
            subscription.PaymentComplete = true; // payment is done

            subscriptionService.AddSubscription(subscription);

            return RedirectToAction("Details", "News");
        }
        // Bayad cod ienja dayel shawad sh. Si?
   
        public IActionResult Index2()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult BuySubscription(int months)
        {
            int typeConverted = Convert.ToInt32(months);
            string buyersName = filipService.GetAppUserByClaimsPrincipal(User).Result.UserName!;

            long narmalPris = 59;
            string stripeName = "1 Month";

            switch (typeConverted)
            {
                case 1:
                {
                    narmalPris = 59;
                    stripeName = "1 Month";
                    break;
                }
                case 3:
                {
                    narmalPris = 168;
                    stripeName = "QUARTER YEAR";
                    break;
                }
                case 6:
                {
                    narmalPris = 318;
                    stripeName = "One Year, Divided by TWO Whole Units, then You get one of the Halfs";
                    break;
                }
                case 12:
                {
                    narmalPris = 531;
                    stripeName = "Uno YEARO";
                    break;
                }
                case 9999:
                {
                    narmalPris = 5899;
                    stripeName = "MY MAN. 833 SPININGS around the SUNe and 0NE Quarter";
                    break;
                }
            }



            // Set Stripe secret key
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

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
                            Currency = "sek",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = stripeName,
                            },
                        },
                        Quantity = 1,
                    }
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:44374/subscription/success/" + "?token=" + GeneratePaymentDetails(buyersName, 1, months*30, narmalPris), //TODO GET DAYS AND SUBTYPE
                CancelUrl = "https://localhost:44374/subscription/cancel"
            };

            // Create session service and session object
            var service = new SessionService();
            Session session = service.Create(options);

            // Redirect to Stripe checkout session URL
            return Redirect(session.Url);
        }
        
        // Success action
        public IActionResult Success(string token)
        {
            Guid gToken = Guid.Parse(token);
            if (gToken == Guid.Empty)
                return RedirectToAction("Index");

            var user = filipService.GetAppUserByClaimsPrincipal(User).Result;
            
            bool redeemed = RedeemToken(gToken, user, subscriptionService);

            if (!redeemed)
                return View(false);
            
            return View(true);
        }

        // Cancel action
        public IActionResult Cancel(string token)
        {
            Guid gToken = Guid.Parse(token);
            RemoveToken(gToken);
            return View();
        }

        public IActionResult ThankYou()
		{
			return View();
		}

        private Guid GeneratePaymentDetails(string userName, int subTypeId, int days, long histPrice)
        {
            Guid guid = Guid.NewGuid();
            AssignToken(guid, userName, subTypeId, days, histPrice);
    
            return guid;
        }
	}
}
