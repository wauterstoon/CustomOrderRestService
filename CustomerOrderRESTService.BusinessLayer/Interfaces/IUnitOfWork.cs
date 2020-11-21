using System;

namespace CustomerOrderRESTService.BusinessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }

        int Complete();
    }
}