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
        public Order AddOrder(int customerId, ProductType product, int amount)
        {
            Customer customer = uow.Customers.Find(customerId);
            if (customer == null) throw new BusinessException("customer doesn't exist");
            
            Order order = customer.CreateOrder(product, amount);
            if(FindOrder(customerId, product) == null)
            {
                uow.Orders.AddOrder(order);
            }
            else
            {
                Order orderUpdate = FindOrder(customerId, product);
                UpdateOrder(orderUpdate.Id, customerId, order.Amount, order.Product);
            }
            uow.Complete();
            return order;
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
        /// Find Order By customerId and product
        /// </summary>
        /// <returns>Order object</returns>
        public Order FindOrder(int customerId, ProductType product)
        {
            return uow.Orders.Find(customerId, product);
        }

        /// <summary>
        /// Update Order, if order exists
        /// </summary>
        //TODO if order is not from that customer
        public void UpdateOrder(int orderId, int customerId, int amount, ProductType product)
        {
            if (FindOrder(orderId) == null) throw new BusinessException("order doesn't exist");
            if (FindCustomer(customerId) == null) throw new BusinessException("customer doesn't exist");
            uow.Orders.UpdateOrder(orderId, customerId, amount, product);
            uow.Complete();
        }

        /// <summary>
        /// Delete Order, if order exists
        /// </summary>
        //TODO if order is not from that customer
        public void DeleteOrder(int orderId, int customerId)
        {
            if (FindOrder(orderId) == null) throw new BusinessException("order doesn't exist");
            if (FindCustomer(customerId) == null) throw new BusinessException("customer doesn't exist");
            uow.Orders.RemoveOrder(orderId, customerId);
            uow.Complete();
        }

        /// <summary>
        /// Add Customer, if the unique combination address and name doesn't exists
        /// </summary>
        public Customer AddCustomer(string name, string address)
        {
            try
            {
            if (uow.Customers.Find(name, address) != null) throw new BusinessException("this combination of name and address already exists");
            Customer customer = new Customer(name, address);
            uow.Customers.AddCustomer(customer);
            uow.Complete();
            return customer;
            }
            catch (Exception)
            {
                return null;
            }

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

            if (uow.Customers.Find(name, address) != null) throw new BusinessException("this combination of name and address already exists");

            uow.Customers.UpdateCustomer(customerId, name, address);
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