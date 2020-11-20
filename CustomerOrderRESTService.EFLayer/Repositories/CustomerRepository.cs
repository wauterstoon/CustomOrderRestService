using CustomerOrderRESTService.BusinessLayer.Interfaces;
using CustomerOrderRESTService.BusinessLayer.Models;
using CustomerOrderRESTService.EFLayer.DataAccess;
using System.Linq;

namespace CustomerOrderRESTService.EFLayer.Repositories
{
    public class CustomerRepository : Repository, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }

        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }

        public Customer Find(int id)
        {
            return context.Customers.Find(id);
        }

        public Customer Find(string name, string address)
        {
            return context.Customers.Where(x => x.Name == name && x.Address == address).FirstOrDefault();
        }

        public void RemoveCustomer(int id)
        {
            Customer customer = context.Customers.Find(id);
            context.Customers.Remove(customer);
        }

        public void UpdateCustomer(int id, string name, string address)
        {
            Customer customer = context.Customers.Find(id);
            customer.Name = name;
            customer.Address = address;
        }
    }
}