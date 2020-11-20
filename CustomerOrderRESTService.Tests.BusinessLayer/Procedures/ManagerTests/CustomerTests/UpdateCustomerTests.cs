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
    public class UpdateCustomerTests
    {
        [TestMethod]
        public void UpdateCustomer_ShouldUpdateNotAddCustomer_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            manager.UpdateCustomer(1, "Tom Janssens", "Nieuwstraat 5, 1000 Brussel");
            // Assert
            ctx.Customers.Count().Should().Be(1);
            Customer customer = ctx.Customers.First();
            Assert.AreEqual(1, customer.Id);
        }

        [TestMethod]
        public void UpdateCustomer_ShouldUpdateCorrectCustomer_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            manager.UpdateCustomer(1, "Tom Janssens", "Nieuwstraat 5, 1000 Brussel");
            // Assert
            Customer customer = ctx.Customers.First();
            Assert.AreEqual("Tom Janssens", customer.Name);
            Assert.AreEqual("Nieuwstraat 5, 1000 Brussel", customer.Address);
        }

        [TestMethod]
        public void UpdateCustomer_ShouldNotUpdateCustomer_IfCustomerDoesNotExist()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            // Act
            try
            {
                manager.UpdateCustomer(1, "Tom Janssens", "Nieuwstraat 5, 1000 Brussel");
            }
            catch (Exception)
            {
            }
            // Assert
            ctx.Customers.Count().Should().Be(0);
        }

        [TestMethod]
        public void UpdateCustomer_ShouldThrowException_IfCustomerDoesNotExist()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            // Act
            Action action = () =>
            {
                manager.UpdateCustomer(1, "Tom Janssens", "Nieuwstraat 5, 1000 Brussel");
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("customer doesn't exist");
        }

        [TestMethod]
        public void UpdateCustomer_ShouldNotUpdateCustomer_IfPropertiesAreNotCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            try
            {
                manager.UpdateCustomer(1, "Tom Janssens", "Nieuwsf");
            }
            catch (Exception)
            {
            }
            // Assert
            Customer customer = ctx.Customers.Local.First();
            Assert.AreEqual("Jan Janssens", customer.Name);
            Assert.AreEqual("Nieuwstraat 100, 1000 Brussel", customer.Address);
        }

        [TestMethod]
        public void UpdateCustomer_ShouldThrowException_IfPropertiesAreNotCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            Action action = () =>
            {
                manager.UpdateCustomer(1, "Tom Janssens", "Nieuwsf");
            };
            action.Should().Throw<BusinessException>().WithMessage("address must be 10 characters or more");
        }
    }
}