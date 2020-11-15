using CustomerOrderRESTService.BusinessLayer.Exceptions;
using CustomerOrderRESTService.BusinessLayer.Interfaces;
using CustomerOrderRESTService.BusinessLayer.Models;
using System;

namespace CustomerOrderRESTService.BusinessLayer.Procedures
{
    public class Manager
    {
        private IUnitOfWork uow;

        public Manager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary>
        /// Add Order, if customers is not null
        /// </summary>
        public void AddOrder(int customerId, ProductType product, int amount)
        {
            Customer customer = uow.Customers.Find(customerId);
            if (customer == null) throw new BusinessException("customer doesn't exist");
            Order order = customer.CreateOrder(product, amount);
            uow.Orders.AddOrder(order);
            uow.Complete();
        }

        /// <summary>
        /// Find Order By ID
        /// </summary>
        /// <returns>Order object</returns>
        public Order FindOrder(int orderId)
        {
            return uow.Orders.Find(orderId);
        }

        /// <summary>
        /// Update Order, if order exists
        /// </summary>
        public void UpdateOrder(int orderId, int amount, ProductType product)
        {
            if (FindOrder(orderId) == null) throw new BusinessException("order doesn't exist");
            uow.Orders.UpdateOrder(orderId, amount, product);
            uow.Complete();
        }

        /// <summary>
        /// Delete Order, if order exists
        /// </summary>
        public void DeleteOrder(int orderId)
        {
            if (FindOrder(orderId) == null) throw new BusinessException("order doesn't exist");
            uow.Orders.RemoveOrder(orderId);
        }

        /// <summary>
        /// Add Customer, if the unique combination address and name doesn't exists
        /// </summary>
        public void AddCustomer(string name, string address)
        {
            string combo = Customer.CreateUniqueNameAddressCombo(name, address);
            if (uow.Customers.Find(combo) != null) throw new BusinessException("this combination of name and address already exists");
            uow.Customers.AddCustomer(new Customer(name, address));
            uow.Complete();
        }

        /// <summary>
        /// Find Customer By ID
        /// </summary>
        /// <returns>Customer object</returns>
        public Customer FindCustomer(int customerId)
        {
            return uow.Customers.Find(customerId);
        }

        /// <summary>
        /// Add Customer, if the unique combination address and name doesn't exists
        /// </summary>
        public void UpdateCustomer(int customerId, string name, string address)
        {
            if (FindCustomer(customerId) == null) throw new BusinessException("customer doesn't exist");

            if (name == String.Empty) throw new BusinessException("name can't be empty");
            if (name == null) throw new NullReferenceException(nameof(name));
            if (address.Length < 10) throw new BusinessException("address must be 10 characters or more");
            if (address == null) throw new NullReferenceException(nameof(address));

            string combo = Customer.CreateUniqueNameAddressCombo(name, address);
            if (uow.Customers.Find(combo) != null) throw new BusinessException("this combination of name and address already exists");

            uow.Customers.UpdateCustomer(customerId, name, address, combo);
            uow.Complete();
        }

        /// <summary>
        /// Delete Customer, if the customer exists en doesn't contains orders
        /// </summary>
        public void DeleteCustomer(int customerId)
        {
            if (FindCustomer(customerId) == null) throw new BusinessException("customer doesn't exist");
            Customer customer = FindCustomer(customerId);
            if (customer.Orders.Count != 0) throw new BusinessException("orderlist still contains orders");
            uow.Customers.RemoveCustomer(customerId);
            uow.Complete();
        }
    }
}