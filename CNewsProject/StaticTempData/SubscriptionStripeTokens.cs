
namespace CNewsProject.StaticTempData;

public static class SubscriptionStripeTokens
{
    private static List<Guid> PaymentTokens { get; set; } = new();
    private static List<PaymentDetail> PaymentDetails { get; set; } = new();
    
    public static void AssignToken(Guid token, string buyer, int subTypeId, int days, long histPrice)
    {
        PaymentTokens.Add(token);
        PaymentDetails.Add(new PaymentDetail(token, buyer, subTypeId, days, histPrice));
    }

    public static void RemoveToken(Guid token)
    {
        PaymentTokens.Remove(token);
        PaymentDetails.RemoveAll(p => p.Id == token);
    }

    public static bool RedeemToken(Guid token, AppUser buyer, ISubscriptionService subscriptionService)
    {
        string buyersName = buyer.UserName!;
        
        if (!PaymentTokens.Contains(token)) return false;
        if (PaymentDetails.Single(pd => pd.Id == token).BuyersName != buyersName) return false;
        
        var details = PaymentDetails.Single(pd => pd.Id == token);
        
        subscriptionService.RedeemSub(buyer, details.SubscriptionTypeId, details.SubDays, details.HistPrice);
        RemoveToken(token);
        return true;
    }
}

internal class PaymentDetail(Guid id, string buyer, int subTypeId, int days, long histPrice)
{
    internal Guid Id { get; set; } = id;
    internal string BuyersName { get; set; } = buyer;
    internal int SubscriptionTypeId { get; set; } = subTypeId;
    internal int SubDays { get; set; } = days;
    internal long HistPrice { get; set; } = histPrice;
}