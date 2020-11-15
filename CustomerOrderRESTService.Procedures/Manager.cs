using CustomerOrderRESTService.BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderRESTService.Procedures
{
    public class Manager
    {
        private IUnitOfWork uow;

        public Manager(IUnitOfWork uow)
        {
            this.uow = uow;
        }
    }
}
