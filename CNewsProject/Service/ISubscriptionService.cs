using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Models.DataBase;


namespace CNewsProject.Service
{
    public interface ISubscriptionService
    {
        // Filips Domän!
        #region Filip's DOMAIN!
        // TYPES
        #region TYPES
        public List<SubscriptionType> GetAllTypes();
        public SubscriptionType GetTypeById(int id);
        public bool TypeHasUsers(int id);
        public bool AddType(SubscriptionType type);
        public bool RemoveType(int id);
        #endregion
        // SUBS
        #region SUBS
        public bool IsSubscribed(AppUser user);
        
        public DateTime GetSubscriptionExpiry(AppUser user);
        public void RedeemSub(AppUser user, int typeId, int days, long histPrice);
        public bool AdminGiveSub(AppUser user, int typeId, double days);
        #endregion
        #endregion




        public List<Subscription> GetAllSubscription();
        public Subscription GetSubscriptionById(int Id);
        public void AddSubscription(Subscription supscription);
        public void RemoveSubscription(Subscription supscription);
        public void EditSubscription(Subscription supscription);
        public Subscription GetSubscriptionByAppUser(ClaimsPrincipal user);

        // for Payment 
        bool IsUserSubscribed(string userId);
    }

}
