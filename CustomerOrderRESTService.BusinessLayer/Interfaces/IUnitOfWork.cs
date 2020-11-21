using CustomerOrderRESTService.BusinessLayer.Models;
using System;

namespace CustomerOrderRESTService.BusinessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }

        void Complete();
        void AddOrderComplete(Order order);
    }
}