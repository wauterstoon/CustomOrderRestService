using CustomerOrderRESTService.BusinessLayer.Interfaces;
using CustomerOrderRESTService.BusinessLayer.Models;
using CustomerOrderRESTService.EFLayer.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace CustomerOrderRESTService.EFLayer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private DataContext context;

        public CustomerRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }

        public Customer Find(int id)
        {
            return context.Customers.Find(id);
        }

        public List<Order> GetOrders (int customerId)
        {
            return context.Orders.Where(x => x.Customer.Id == customerId).ToList();
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