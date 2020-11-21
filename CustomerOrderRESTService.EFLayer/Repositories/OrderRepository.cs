using CustomerOrderRESTService.BusinessLayer.Interfaces;
using CustomerOrderRESTService.BusinessLayer.Models;
using CustomerOrderRESTService.EFLayer.DataAccess;

namespace CustomerOrderRESTService.EFLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private DataContext context;

        public OrderRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddOrder(Order order)
        {
            context.Orders.Add(order);
        }

        public Order Find(int id)
        {
            return context.Orders.Find(id);
        }

        public void RemoveOrder(int id)
        {
            Order order = context.Orders.Find(id);
            Customer customer = order.Customer;
            customer.RemoveOrder(id);
            context.Orders.Remove(order);
        }

        public void UpdateOrder(int id, int amount, ProductType product)
        {
            Order order = context.Orders.Find(id);
            Customer customer = order.Customer;
            customer.ChangeOrder(order, product, amount);
            order.Product = product;
        }
    }
}