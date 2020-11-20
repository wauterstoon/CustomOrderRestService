using CustomerOrderRESTService.BusinessLayer.Models;

namespace CustomerOrderRESTService.BusinessLayer.Procedures
{
    public interface IManager
    {
        void AddCustomer(string name, string address);
        void AddOrder(int customerId, ProductType product, int amount);
        void DeleteCustomer(int customerId);
        void DeleteOrder(int orderId);
        Customer FindCustomer(int customerId);
        Order FindOrder(int orderId);
        void UpdateCustomer(int customerId, string name, string address);
        void UpdateOrder(int orderId, int amount, ProductType product);
    }
}