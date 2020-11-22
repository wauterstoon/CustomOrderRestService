using CustomerOrderRESTService.BusinessLayer.Models;

namespace CustomerOrderRESTService.BusinessLayer.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);

        void UpdateOrder(int orderId, int customerId, int amount, ProductType product);

        void RemoveOrder(int orderId, int customerId);

        Order Find(int id);
        Order Find(int customerId, ProductType product);
    }
}