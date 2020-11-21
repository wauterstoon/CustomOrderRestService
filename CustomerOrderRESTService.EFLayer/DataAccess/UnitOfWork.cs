using CustomerOrderRESTService.BusinessLayer.Interfaces;
using CustomerOrderRESTService.BusinessLayer.Models;
using CustomerOrderRESTService.EFLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace CustomerOrderRESTService.EFLayer.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;

        public UnitOfWork(DataContext context)
        {
            this.context = context;

            Customers = new CustomerRepository(this.context);
            Orders = new OrderRepository(this.context);
        }

        public ICustomerRepository Customers { get; private set; }
        public IOrderRepository Orders { get; private set; }

        public void AddOrderComplete(Order order)
        {
            try
            {
                context.Orders.Add(order);

                context.Database.BeginTransaction();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[OrderTable] ON");

                context.SaveChanges();

                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[OrderTable] OFF");

                context.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw;
            }
        }

        public void Complete()
        {
            try
            {
                //context.Database.BeginTransaction();
                //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[OrderTable] ON");

                context.SaveChanges();

                //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[OrderTable] OFF");

                //context.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}