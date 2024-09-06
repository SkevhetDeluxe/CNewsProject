using Stripe;
namespace CNewsProject.Service
{
    public class StripeService
    {
        public StripeService()
        {
            StripeConfiguration.ApiKey = "sk_test_51PpQ0VESeNuo0minJ1HfA6LT2DSCgtu3x7ISK8FA2B2jreiiwRFpuRGLIRDdOzRykaUWgM8qbU92E8rfomCWLAx900VnBOUypI";
        }

        public async Task<PaymentIntent> CreatePaymentIntent(long amount, string currency, string paymentMethodId, string returnUrl)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = currency,
                PaymentMethod = paymentMethodId,
                ConfirmationMethod = "manual",
                Confirm = true,
                ReturnUrl = returnUrl
            };

            var service = new PaymentIntentService();
            return await service.CreateAsync(options);
        }
    }
}
