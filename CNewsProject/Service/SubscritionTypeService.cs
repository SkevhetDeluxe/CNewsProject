using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Data;
using CNewsProject.Models.DataBase;

namespace CNewsProject.Service
{
    public class SubscriptionTypeService : ISubscriptionTypeService
    {

        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public SubscriptionTypeService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }


        public List<SubscriptionType> GetAllSubscriptionType()
        {
            return _db.SubscriptionType.OrderBy(o => o.Id).ToList();
        }

        public SubscriptionType GetSubscriptionTypeById(int id)
        {
            return _db.SubscriptionType.FirstOrDefault(o => o.Id == id);
        }


        public void AddSubscriptionType(SubscriptionType subscriptiontype)
        {
            _db.SubscriptionType.Add(subscriptiontype);
            _db.SaveChanges();

        }
        public void RemoveSubscriptionType(SubscriptionType subscriptiontype)
        {
            _db.SubscriptionType.Remove(_db.SubscriptionType.FirstOrDefault(m => m.Id == subscriptiontype.Id));
            _db.SaveChanges();

        }

        public void EditSubscriptionType(SubscriptionType subscriptiontype)
        {
            GetSubscriptionTypeById(subscriptiontype.Id).TypeName = subscriptiontype.TypeName;
            GetSubscriptionTypeById(subscriptiontype.Id).Description = subscriptiontype.Description;
            GetSubscriptionTypeById(subscriptiontype.Id).Price = subscriptiontype.Price;
           
            _db.SaveChanges();
        }


    }

}