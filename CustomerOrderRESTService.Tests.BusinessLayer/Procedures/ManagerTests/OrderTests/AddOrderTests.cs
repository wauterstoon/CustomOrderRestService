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
    public class AddOrderTests
    {
        [TestMethod]
        public void AddOrder_ShouldAdd1Order_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Assert
            ctx.Orders.Count().Should().Be(1);
        }

        [TestMethod]
        public void AddOrder_ShouldAddCorrectOrder_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Assert
            Order order = ctx.Orders.Local.First();
            Assert.AreEqual(1, order.Id);
            Assert.AreEqual(5, order.Amount);
            Assert.AreEqual(ProductType.Duvel, order.Product);
        }

        [TestMethod]
        public void AddOrder_ShouldUpdateCustomerOrdersList_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Assert
            Customer customer = ctx.Customers.Local.First();
            Order order = customer.Orders.First();
            Assert.AreEqual(1, customer.Orders.Count);
            Assert.AreEqual(ProductType.Duvel, order.Product);
            Assert.AreEqual(5, order.Amount);
            Assert.AreEqual(1, order.Customer.Id);
            Assert.AreEqual("Jan Janssens", order.Customer.Name);
        }

        [TestMethod]
        public void AddOrder_ShouldAddCustomerToOrder_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Assert
            Order order = ctx.Orders.Local.First();
            Assert.AreEqual(1, order.Customer.Id);
            Assert.AreEqual("Jan Janssens", order.Customer.Name);
        }

        [TestMethod]
        public void AddOrder_ShouldNotAddOrder_IfPropertiesAreNotCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            try
            {
                manager.AddOrder(1, ProductType.Duvel, 0);
            }
            catch (Exception)
            {
            }
            // Assert
            ctx.Orders.Count().Should().Be(0);
        }

        [TestMethod]
        public void AddOrder_ShouldThrowException_IfPropertiesAreNotCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            Action action = () =>
            {
                manager.AddOrder(1, ProductType.Duvel, 0);
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("amount must be more than 1");
        }

        [TestMethod]
        public void AddOrder_ShouldNotAddOrder_IfCustomerDoesNotExist()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            // Act
            try
            {
                manager.AddOrder(1, ProductType.Duvel, 0);
            }
            catch (Exception)
            {
            }
            // Assert
            ctx.Orders.Count().Should().Be(0);
        }

        [TestMethod]
        public void AddOrder_ShouldThrowException_IfCustomerDoesNotExist()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            // Act
            Action action = () =>
            {
                manager.AddOrder(1, ProductType.Duvel, 5);
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("customer doesn't exist");
        }
    }
}