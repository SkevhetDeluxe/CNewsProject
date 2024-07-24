using CNewsProject.Models.DataBase;

namespace CNewsProject.Service
{
    public interface ICustomerService
    {

        public List<Customer> GetAllCustomers();
        public Customer GetCustomerById(int Id);
        public void AddCustomer(Customer customer);
        public void RemoveCustomer(Customer customer);
        public void EditCustomer(Customer customer);

        public Customer GetCustomerByEmail(string email);
    }
}
