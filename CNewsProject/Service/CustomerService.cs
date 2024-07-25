using CNewsProject.Models.DataBase;
using CNewsProject.Data;

namespace CNewsProject.Service
{
    public class CustomerService:ICustomerService
    {
        private readonly CNewsDbContext _db;
        private readonly IConfiguration _configuration;

        public CustomerService(CNewsDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public List<Customer> GetAllCustomers()
        {
            return _db.Customers.OrderBy(m => m.EmailAddress).ToList();
        }

        public Customer GetCustomerById(int Id)
        {
            return (_db.Customers.FirstOrDefault(m => m.Id == Id)!);
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _db.Customers.First(c => c.EmailAddress == email);
        }

        public void AddCustomer(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }

        public void RemoveCustomer(Customer customer)
        {
            _db.Customers.Remove(_db.Customers.FirstOrDefault(c => c.Id == customer.Id)!);
            _db.SaveChanges();

        }




        public void EditCustomer(Customer customer)
        {
            GetCustomerById(customer.Id).FirstName = customer.FirstName;
            GetCustomerById(customer.Id).LastName = customer.LastName;
            GetCustomerById(customer.Id).EmailAddress = customer.EmailAddress;
            GetCustomerById(customer.Id).Mobilenumber = customer.Mobilenumber;
            GetCustomerById(customer.Id).Password = customer.Password;
            GetCustomerById(customer.Id).Subscriptions = customer.Subscriptions;

            _db.SaveChanges();
        }

    }
}
