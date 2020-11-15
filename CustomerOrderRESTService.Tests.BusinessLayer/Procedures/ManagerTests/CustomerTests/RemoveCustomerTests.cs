using CustomerOrderRESTService.BusinessLayer.Exceptions;
using CustomerOrderRESTService.BusinessLayer.Models;
using CustomerOrderRESTService.BusinessLayer.Procedures;
using CustomerOrderRESTService.EFLayer.DataAccess;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CustomerOrderRESTService.Tests.BusinessLayer.Procedures.ManagerTests.CustomerTests
{
    [TestClass]
    public class RemoveCustomerTests
    {
        [TestMethod]
        public void DeleteCustomer_ShouldDeleteCustomer_IfCustomerExists()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            manager.DeleteCustomer(1);
            // Assert
            ctx.Customers.Count().Should().Be(0);
        }

        [TestMethod]
        public void DeleteCustomer_ShouldNotDeleteCustomer_IfCustomerHasOrders()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            try
            {
                manager.DeleteCustomer(1);
            }
            catch (Exception)
            {
            }
            // Assert
            ctx.Customers.Count().Should().Be(1);
        }

        [TestMethod]
        public void DeleteCustomer_ShouldThrowException_IfCustomerHasOrders()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            manager.AddOrder(1, ProductType.Duvel, 5);
            // Act
            Action action = () =>
            {
                manager.DeleteCustomer(1);
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("orderlist still contains orders");
        }

        [TestMethod]
        public void DeleteCustomer_ShouldNotDeleteCustomer_IfCustomerDoesNotExists()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            try
            {
                manager.DeleteCustomer(2);
            }
            catch (Exception)
            {
            }
            // Assert
            ctx.Customers.Count().Should().Be(1);
        }

        [TestMethod]
        public void DeleteCustomer_ShouldThrowException_IfCustomerDoesNotExists()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            Action action = () =>
            {
                manager.DeleteCustomer(2);
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("customer doesn't exist");
        }
    }
}