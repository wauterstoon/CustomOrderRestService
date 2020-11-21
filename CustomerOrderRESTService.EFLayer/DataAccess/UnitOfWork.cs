using CustomerOrderRESTService.BusinessLayer.Interfaces;
using CustomerOrderRESTService.EFLayer.Repositories;
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

        public int Complete()
        {
            try
            {
                return context.SaveChanges();
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