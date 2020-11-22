using CustomerOrderRESTService.BusinessLayer.Interfaces;
using CustomerOrderRESTService.BusinessLayer.Models;
using CustomerOrderRESTService.EFLayer.DataAccess;
using System.Linq;

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
        
        public Order Find (int id)
        {
            return context.Orders.Find(id);
        }

        public Order Find(int customerId, ProductType product)
        {
            return context.Orders.Where(x => x.Customer.Id == customerId && x.Product.Equals(product)).FirstOrDefault();
        }

        public void RemoveOrder(int orderId, int customerId)
        {
            Order order = context.Orders.Find(orderId);
            Customer customer = context.Customers.Find(customerId);
            customer.RemoveOrder(orderId);
            context.Orders.Remove(order);
        }

        public void UpdateOrder(int orderId, int customerId, int amount, ProductType product)
        {
            Order order = context.Orders.Find(orderId);
            Customer customer = context.Customers.Find(customerId);
            Order orderUpdated = customer.ChangeOrder(order, product, amount);
            context.Orders.Update(orderUpdated);
           // order.Product = product;
        }
    }
}