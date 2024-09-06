using Stripe;
using CNewsProject.Service;
using Microsoft.AspNetCore.Mvc;
namespace CNewsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly StripeService _stripeService;

        public PaymentController(StripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpGet("index", Name = "PaymentIndex")]
        public IActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("Membership");
        }

        [HttpGet("success", Name = "PaymentSuccess")]
        public IActionResult PaymentSuccess()
        {
            return View();
        }

        [HttpPost("charge")]
        public async Task<IActionResult> Charge([FromBody] MembershipRequest request, string? returnUrl)
        {
            returnUrl = returnUrl ?? "https://localhost:7200/";

            long amount = GetAmountForMembership(request.MembershipType, request.PaymentFrequency);

            var options = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = request.Currency,
                PaymentMethod = request.PaymentMethodId,
                ConfirmationMethod = "manual",
                Confirm = true,
                ReturnUrl = returnUrl
            };

            var service = new PaymentIntentService();
            try
            {
                PaymentIntent intent = await service.CreateAsync(options);

                if (intent.Status == "succeeded")
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, error = "Payment failed" });
                }
            }
            catch (StripeException e)
            {
                return Json(new { success = false, error = e.StripeError.Message });
            }
        }



        private long GetAmountForMembership(string membershipType, string paymentFrequency)
        {
            long amount = 0;
            if (paymentFrequency == "monthly")
            {
                switch (membershipType)
                {
                    case "basic":
                        amount = 2900; // $29/month
                        break;
                    case "middle":
                        amount = 5900; // $59/month
                        break;
                    case "expensive":
                        amount = 18900; // $189/month
                        break;
                }
            }
            else if (paymentFrequency == "yearly")
            {
                switch (membershipType)
                {
                    case "basic":
                        amount = 29000; // $290/year (example)
                        break;
                    case "middle":
                        amount = 59000; // $590/year (example)
                        break;
                    case "expensive":
                        amount = 189000; // $1890/year (example)
                        break;
                }
            }
            return amount;
        }
    }

    public class MembershipRequest
    {
        public string MembershipType { get; set; } // e.g., "basic", "middle", "expensive"
        public string PaymentFrequency { get; set; } // "monthly" or "yearly"
        public string Currency { get; set; }
        public string PaymentMethodId { get; set; }
    }
}

