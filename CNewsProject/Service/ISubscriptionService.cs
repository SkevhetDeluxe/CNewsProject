using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Models.DataBase;


namespace CNewsProject.Service
{
    public interface ISubscriptionService
    {

        public List<Subscription> GetAllSubscription();
        public Subscription GetSubscriptionById(int Id);
        public void AddSubscription(Subscription supscription);
        public void RemoveSubscription(Subscription supscription);
        public void EditSubscription(Subscription supscription);

        // for Payment 
        bool IsUserSubscribed(string userId);
    }
}
