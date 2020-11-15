using CustomerOrderRESTService.BusinessLayer.Interfaces;
using CustomerOrderRESTService.BusinessLayer.Procedures;
using CustomerOrderRESTService.EFLayer.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderRESTService.CommonLayer
{
    public class ManagerFactory
    {
        public static Manager CreateManager(string targetDB)
        {
            return new Manager(new UnitOfWork(new DataContext(targetDB)));
        }
    }
}
