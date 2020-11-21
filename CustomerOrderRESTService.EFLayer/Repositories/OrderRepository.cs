using CustomerOrderRESTService.BusinessLayer.Interfaces;
using CustomerOrderRESTService.BusinessLayer.Models;
using CustomerOrderRESTService.EFLayer.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrderRESTService.EFLayer.Repositories
{
    public class OrderRepository : Repository, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }

        //public void AddOrder(Order order)
        //{
        //    context.Orders.Add(order);

        //    context.Database.BeginTransaction();
        //    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[OrderTable] ON");

        //    context.SaveChanges();

        //    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[OrderTable] OFF");

        //    context.Database.CommitTransaction();
        //}

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