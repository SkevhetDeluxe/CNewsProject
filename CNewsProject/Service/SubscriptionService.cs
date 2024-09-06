using CNewsProject.Data;
using CNewsProject.Models.DataBase;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CNewsProject.Service
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public SubscriptionService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        // Filips Domän!
        #region Filips DOMAIN!

        // TYPES
        #region TYPES
        public List<SubscriptionType> GetAllTypes()
        {
            return _db.SubscriptionType.OrderBy(s => s.Id).ToList();
        }

        public SubscriptionType GetTypeById(int id)
        {
            return _db.SubscriptionType.Single(t => t.Id == id);
        }

        public bool TypeHasUsers(int id)
        {
            return _db.Subscription.Any(s => s.SubscriptionType == _db.SubscriptionType.Single(t => t.Id == id));
        }

        public bool AddType(SubscriptionType type)
        {
            try
            {
                _db.SubscriptionType.Add(type);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveType(int id)
        {
            try
            {
                _db.SubscriptionType.Remove(GetTypeById(id));
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
        // SUBSCRIPTIONS
        #region SUBSCRIPTIONS

        public bool AdminGiveSub(AppUser user, int typeId, double days)
        {
            try
            {
                Subscription newSub = new()
                {
                    SubscriptionType = GetTypeById(typeId),
                    HistoricalPrice = GetTypeById(typeId).Price,
                    ExpiresDate = DateTime.Now.AddDays(days),
                    User = user,
                    PaymentComplete = true
                };

                _db.Subscription.Add(newSub);
                _db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #endregion


        // sh
        public bool IsUserSubscribed(string userId)
        {
            try
            {
                var subscription = _db.Subscription.FirstOrDefault(s => s.UserId == userId && s.ExpiresDate > DateTime.Now);
                return subscription != null && subscription.PaymentComplete;

            }
            catch
            {
                return false;
            }
        }
        // sh
       
        public List<Subscription> GetAllSubscription()
        {
            return _db.Subscription.OrderBy(o => o.Id).ToList();
        }

        public Subscription GetSubscriptionById(int id)
        {
            return _db.Subscription.FirstOrDefault(o => o.Id == id);
        }


        public void AddSubscription(Subscription subscription)
        {
            _db.Subscription.Add(subscription);
            _db.SaveChanges();

        }
        public void RemoveSubscription(Subscription subscription)
        {
            _db.Subscription.Remove(_db.Subscription.FirstOrDefault(m => m.Id == subscription.Id));
            _db.SaveChanges();

        }

        public void EditSubscription(Subscription subscription)
        {
            GetSubscriptionById(subscription.Id).SubscriptionType = subscription.SubscriptionType;
            GetSubscriptionById(subscription.Id).HistoricalPrice = subscription.HistoricalPrice;
            //GetSubscriptionById(subscription.Id).CreateDate = subscription.CreateDate;
            GetSubscriptionById(subscription.Id).ExpiresDate = subscription.ExpiresDate;
            GetSubscriptionById(subscription.Id).UserId = subscription.UserId;
            GetSubscriptionById(subscription.Id).PaymentComplete = subscription.PaymentComplete; 

            _db.SaveChanges();
        }

       
    }

}