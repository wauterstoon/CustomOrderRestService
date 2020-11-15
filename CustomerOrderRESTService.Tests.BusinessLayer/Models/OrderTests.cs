using CustomerOrderRESTService.BusinessLayer.Exceptions;
using CustomerOrderRESTService.BusinessLayer.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CustomerOrderRESTService.Tests.BusinessLayer.Models
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void CreateOrder_ShouldMakeOrderObject_IfPropertiesAreCorrect()
        {
            // Arrange
            Customer customer = new Customer("Jan Janssens", "Sint-Veerleplein 11, 9000 Gent");
            // Act
            customer.CreateOrder(ProductType.Duvel, 10);
            // Assert
            Order order = customer.Orders.FirstOrDefault();
            Assert.IsInstanceOfType(order, typeof(Order));
            Assert.AreEqual(ProductType.Duvel, order.Product);
            Assert.AreEqual(10, order.Amount);
            Assert.AreEqual("Jan Janssens", customer.Name);
        }

        [TestMethod]
        public void CreateOrder_ShouldNotThrowException_IfPropertiesAreCorrect()
        {
            // Arrange
            Customer customer = new Customer("Jan Janssens", "Sint-Veerleplein 11, 9000 Gent");
            // Act
            Action action = () =>
            {
                customer.CreateOrder(ProductType.Duvel, 10);
            };
            // Assert
            action.Should().NotThrow<Exception>();
        }

        [TestMethod]
        public void CreateOrder_ShouldThrowOrderException_IfAmountIsNotMoreThen1()
        {
            // Arrange
            Customer customer = new Customer("Jan Janssens", "Sint-Veerleplein 11, 9000 Gent");
            // Act
            Action action = () =>
            {
                customer.CreateOrder(ProductType.Duvel, 1);
            };
            // Assert
            action.Should().Throw<BusinessException>().WithMessage("amount must be more than 1");
        }

        [TestMethod]
        public void CreateOrder_ShouldThrowNullReferenceException_IfCustomerIsNull()
        {
            // Arrange
            Customer customer = null;
            // Act
            Action action = () =>
            {
                customer.CreateOrder(ProductType.Duvel, 10);
            };
            // Assert
            action.Should().Throw<NullReferenceException>();
        }
    }
}