using CustomerOrderRESTService.BusinessLayer.Models;
using CustomerOrderRESTService.BusinessLayer.Procedures;
using CustomerOrderRESTService.EFLayer.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderRESTService.Tests.BusinessLayer.Procedures.ManagerTests.OrderTests
{
    [TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void MethodName_condition_expectedValue()
        {
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.UpdateCustomer(1, "Tom Janssens", "Nieuwstraat 5, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            manager.AddOrder(1, ProductType.Duvel, 5);
            manager.AddOrder(1, ProductType.Duvel, 5);
            manager.AddOrder(1, ProductType.Leffe, 10);

            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(2, ProductType.Duvel, 5);

            manager.DeleteOrder(1);

            manager.FindOrder(4);

            manager.DeleteOrder(2);
            manager.DeleteOrder(3);
            manager.DeleteOrder(4);

            manager.FindCustomer(2);

            manager.DeleteCustomer(1);
        }   
    }
}
