using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace CNewsProject.Controllers
{
    public class PaymentController : Controller
    {
        private readonly string _secretKey = "sk_test_51PpQ0VESeNuo0minJ1HfA6LT2DSCgtu3x7ISK8FA2B2jreiiwRFpuRGLIRDdOzRykaUWgM8qbU92E8rfomCWLAx900VnBOUypI"; 

        public PaymentController()
        {
            StripeConfiguration.ApiKey = _secretKey;
        }

        [HttpPost]
        public async Task<IActionResult> Charge([FromBody] TokenRequest tokenRequest)
        {
            var options = new ChargeCreateOptions
            {
                Amount = 2000, 
                Currency = "usd",
                Description = "Example charge",
                Source = tokenRequest.TokenId,
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            return Json(new { success = charge.Status == "succeeded" });
        }
    }
    public class TokenRequest
    {
        public string TokenId { get; set; }
    }
}
