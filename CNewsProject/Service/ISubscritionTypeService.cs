using Microsoft.AspNetCore.Mvc.Rendering;
using CNewsProject.Models.DataBase;

namespace CNewsProject.Service
{
    public interface ISubscriptionTypeService
    {

        public List<SubscriptionType> GetAllSubscriptionType();
        public SubscriptionType GetSubscriptionTypeById(int Id);
        public void AddSubscriptionType(SubscriptionType supscriptiontype);
        public void RemoveSubscriptionType(SubscriptionType supscriptiontype);
        public void EditSubscriptionType(SubscriptionType supscriptiontype);

    }
}
