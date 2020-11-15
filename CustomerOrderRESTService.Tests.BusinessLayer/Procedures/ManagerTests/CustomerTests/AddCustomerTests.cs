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
    public class AddCustomerTests
    {
        [TestMethod]
        public void AddCustomer_ShouldAddCorrectCustomer_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            // Act
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Assert
            Customer customer = ctx.Customers.Local.First();
            Assert.AreEqual("Jan Janssens", customer.Name);
            Assert.AreEqual("Nieuwstraat 100, 1000 Brussel", customer.Address);
            Assert.AreEqual("JanJanssensNieuwstraat100,1000Brussel", customer.UniqueNameAddressCombo);
        }

        [TestMethod]
        public void AddCustomer_ShouldAdd1Customer_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            // Act
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Assert
            ctx.Customers.Count().Should().Be(1);
        }

        [TestMethod]
        public void AddCustomer_ShouldBeTypeOffCustomer_IfPropertiesAreCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            // Act
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Assert
            Customer customer = ctx.Customers.Local.First();
            Assert.IsInstanceOfType(customer, typeof(Customer));
        }

        [TestMethod]
        public void AddCustomer_ShouldThrowException_IfPropertiesAreNotCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            // Act
            Action action = () =>
            {
                manager.AddCustomer("Jan Janssens", "Straat");
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("address must be 10 characters or more");
        }

        [TestMethod]
        public void AddCustomer_ShouldNotAddCustomer_IfPropertiesAreNotCorrect()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            // Act
            try
            {
                manager.AddCustomer("Jan Janssens", "Straat");
            }
            catch (Exception)
            {
            }
            // Assert
            ctx.Customers.Count().Should().Be(0);
        }

        [TestMethod]
        public void AddCustomer_ShouldThrowException_IfNameAddressComboAlreadyExists()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            Action action = () =>
            {
                manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("this combination of name and address already exists");
        }

        [TestMethod]
        public void AddCustomer_ShouldNotAddCustomer_IfNameAddressComboAlreadyExists()
        {
            // Arrange
            DataContextTests ctx = new DataContextTests(keepExistingDB: false);
            Manager manager = new Manager(new UnitOfWork(ctx));
            manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            // Act
            try
            {
                manager.AddCustomer("Jan Janssens", "Nieuwstraat 100, 1000 Brussel");
            }
            catch (Exception)
            {
            }
            // Assert
            ctx.Customers.Count().Should().Be(1);
        }
    }
}