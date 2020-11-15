using CustomerOrderRESTService.BusinessLayer.Exceptions;
using CustomerOrderRESTService.BusinessLayer.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CustomerOrderRESTService.Tests.BusinessLayer.Models
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void CreateCustomer_ShouldMakeCustomerObject_IfPropertiesAreCorrect()
        {
            // Arrange
            // Act
            Customer customer = new Customer("Jan Janssens", "Sint-Veerleplein 11, 9000 Gent");
            // Assert
            Assert.IsInstanceOfType(customer, typeof(Customer));
            Assert.AreEqual("Jan Janssens", customer.Name);
            Assert.AreEqual("Sint-Veerleplein 11, 9000 Gent", customer.Address);
            Assert.AreEqual("JanJanssensSint-Veerleplein11,9000Gent", customer.UniqueNameAddressCombo);
        }

        [TestMethod]
        public void CreateCustomer_ShouldNotThrowException_IfPropertiesAreCorrect()
        {
            // Arrange
            // Act
            Action action = () =>
            {
                new Customer("Jan Janssens", "Sint-Veerleplein 11, 9000 Gent");
            };
            // Assert
            action.Should().NotThrow<Exception>();
        }

        [TestMethod]
        public void CreateCustomer_ShouldThrowNullReferenceException_IfNameIsNull()
        {
            // Arrange
            // Act
            Action action = () =>
            {
                new Customer(null, "Sint-Veerleplein 11, 9000 Gent");
            };
            // Assert
            action.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void CreateCustomer_ShouldThrowNullReferenceException_IfAddressIsNull()
        {
            // Arrange
            // Act
            Action action = () =>
            {
                new Customer("Jan Janssens", null);
            };
            // Assert
            action.Should().Throw<NullReferenceException>();
        }

        [TestMethod]
        public void CreateCustomer_ShouldThrowCustomerException_IfNameIsEmpty()
        {
            // Arrange
            // Act
            Action action = () =>
            {
                new Customer("", "Sint-Veerleplein 11, 9000 Gent");
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("name can't be empty");
        }

        [TestMethod]
        public void CreateCustomer_ShouldThrowCustomerException_IfAddressLengthIsLessThan10()
        {
            // Arrange
            // Act
            Action action = () =>
            {
                new Customer("Jan Jansens", "Sint-Veer");
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("address must be 10 characters or more");
        }

        [TestMethod]
        public void AddOrder_ShouldAddOrder_IfProductypeDoesNotExistInOrderList()
        {
            // Arrange
            Customer customer = new Customer("Jan Jansens", "Sint-Veerleplein 11, 9000 Gent");
            // Act
            Action action = () =>
            {
                customer.CreateOrder(ProductType.Duvel, 5);
            };
            // Assert
            action.Should().NotThrow<Exception>();
            customer.Orders.Count.Should().Be(1);
        }

        [TestMethod]
        public void AddOrder_ShouldNotAddOrder_IfProductypeExistsInOrderList()
        {
            // Arrange
            Customer customer = new Customer("Jan Jansens", "Sint-Veerleplein 11, 9000 Gent");
            customer.CreateOrder(ProductType.Duvel, 5);
            // Act
            Action action = () =>
            {
                customer.CreateOrder(ProductType.Duvel, 5);
            };
            // Assert
            action.Should().NotThrow<Exception>();
            customer.Orders.Count.Should().Be(1);
        }

        [TestMethod]
        public void AddOrder_ShouldUpdateOrderAmount_IfProductypeExistsInOrderList()
        {
            // Arrange
            Customer customer = new Customer("Jan Jansens", "Sint-Veerleplein 11, 9000 Gent");
            customer.CreateOrder(ProductType.Duvel, 5);
            // Act
            customer.CreateOrder(ProductType.Duvel, 5);
            // Assert
            customer.Orders.First().Amount.Should().Be(10);
        }
    }
}