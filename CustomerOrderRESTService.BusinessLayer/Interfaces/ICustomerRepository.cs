using CustomerOrderRESTService.BusinessLayer.Models;
using System.Collections.Generic;

namespace CustomerOrderRESTService.BusinessLayer.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);

        void UpdateCustomer(int id, string name, string address);

        void RemoveCustomer(int id);

        Customer Find(int id);

        List<Order> GetOrders(int id); 

        Customer Find(string name, string address);
    }
}