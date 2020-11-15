using CustomerOrderRESTService.BusinessLayer.Exceptions;
using CustomerOrderRESTService.BusinessLayer.Models;
using CustomerOrderRESTService.BusinessLayer.Procedures;
using CustomerOrderRESTService.EFLayer.DataAccess;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CustomerOrderRESTService.Tests.BusinessLayer.Procedures.ManagerTests.OrderTests
{
    [TestClass]
    public class RemoveOrderTests
    {
        [TestMethod]
        public void DeleteOrder_ShouldDeleteOrder_IfOrderExists()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            try
            {
                manager.DeleteOrder(1);
            }
            catch (Exception)
            {
            }
            // Assert
            ctx.Orders.Local.Count().Should().Be(0);
        }

        [TestMethod]
        public void DeleteOrder_ShouldDeleteOrderInCustomerOrdersList_IfOrderExists()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            manager.DeleteOrder(1);
            // Assert
            Customer customer = ctx.Customers.Local.First();
            Assert.AreEqual(0, customer.Orders.Count());
        }

        [TestMethod]
        public void DeleteOrder_ShouldNotDeleteOrder_IfOrderDoesNotExist()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            try
            {
                manager.DeleteOrder(2);
            }
            catch (Exception)
            {
            }
            // Assert
            ctx.Orders.Local.Count().Should().Be(1);
        }

        [TestMethod]
        public void DeleteOrder_ShouldThrowException_IfOrderDoesNotExist()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            Action action = () =>
            {
                manager.DeleteOrder(2);
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("order doesn't exist");
        }
    }
}