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
    public class UpdateOrderTests
    {
        [TestMethod]
        public void UpdateOrder_ShouldUpdate1Order_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            manager.UpdateOrder(1, 10, ProductType.Leffe);
            // Assert
            ctx.Orders.Count().Should().Be(1);
        }

        [TestMethod]
        public void UpdateOrder_ShouldUpdateCorrectOrder_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            manager.UpdateOrder(1, 10, ProductType.Leffe);
            // Assert
            Order order = ctx.Orders.First();
            Assert.AreEqual(10, order.Amount);
            Assert.AreEqual(ProductType.Leffe, order.Product);
        }

        [TestMethod]
        public void UpdateOrder_ShouldChangeAmount_IfProductTypeAlreadyExist()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            manager.UpdateOrder(1, 10, ProductType.Duvel);
            // Assert
            Order order = ctx.Orders.First();
            Assert.AreEqual(15, order.Amount);
            Assert.AreEqual(ProductType.Duvel, order.Product);
        }

        [TestMethod]
        public void UpdateOrder_ShouldUpdateCustomerOrdersList_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            manager.UpdateOrder(1, 10, ProductType.Leffe);
            // Assert
            Order order = ctx.Orders.First();
            Order orderInList = order.Customer.Orders.First();
            Assert.AreEqual(10, orderInList.Amount);
            Assert.AreEqual(ProductType.Leffe, orderInList.Product);
        }

        [TestMethod]
        public void UpdateOrder_ShouldUpdateCustomerOrdersList_IfProductTypeAlreadyExist()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            manager.UpdateOrder(1, 10, ProductType.Duvel);
            // Assert
            Order order = ctx.Orders.First();
            Order orderInList = order.Customer.Orders.First();
            Assert.AreEqual(15, orderInList.Amount);
            Assert.AreEqual(ProductType.Duvel, orderInList.Product);
        }

        [TestMethod]
        public void UpdateOrder_ShouldNotUpdateOrder_IfPropertiesAreNotCorrect()
        {
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            try
            {
                manager.UpdateOrder(1, 0, ProductType.Leffe);
            }
            catch (Exception)
            {
            }
            // Assert
            Order order = ctx.Orders.First();
            Assert.AreEqual(5, order.Amount);
            Assert.AreEqual(ProductType.Duvel, order.Product);
        }

        [TestMethod]
        public void UpdateOrder_ShouldThrowException_IfPropertiesAreNotCorrect()
        {
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            Action action = () =>
            {
                manager.UpdateOrder(1, 0, ProductType.Leffe);
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("amount must be more than 1");
        }
    }
}