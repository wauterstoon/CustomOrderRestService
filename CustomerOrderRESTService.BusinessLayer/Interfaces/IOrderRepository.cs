using CustomerOrderRESTService.BusinessLayer.Models;

namespace CustomerOrderRESTService.BusinessLayer.Interfaces
{
    public interface IOrderRepository
    {
        //void AddOrder(Order order);

        void UpdateOrder(int id, int amount, ProductType product);

        void RemoveOrder(int id);

        Order Find(int id);
    }
}