using CustomerOrderRESTService.BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerOrderRESTService.BusinessLayer.Models
{
    public class Customer
    {
        #region Fields
        private int _id;
        private string _name;
        private string _address;
        private List<Order> _orders = new List<Order>();

        #endregion Fields

        #region Properties

        public int Id
        {
            get { return _id; }
            set
            {
                if (value < -1 ) throw new BusinessException("id can't be lower than 0");
                _id = value;
            }
        }
        public string Name {
            get { return _name; }
            set
            {
                if (value == null) throw new ArgumentNullException("name can not be null");
                if (value == String.Empty) throw new BusinessException("name can't be empty");
                _name = value;
            }
        }
        public string Address {
            get { return _address; }
            set
            {
                if (value == null) throw new ArgumentNullException("address can not be null");
                if (value.Length < 10) throw new BusinessException("address must be 10 characters or more");
                _address = value;
            }
        }

        public IReadOnlyList<Order> Orders
        {
            get { return _orders.AsReadOnly(); }
        }

        #endregion Properties

        #region Constructors

        internal Customer()
        {
        }

        public Customer(string name, string address)
        {
            if (name == String.Empty) throw new BusinessException("name can't be empty");
            Name = name ?? throw new NullReferenceException(nameof(name));
            if (address.Length < 10) throw new BusinessException("address must be 10 characters or more");
            Address = address ?? throw new NullReferenceException(nameof(address));
        }

        #endregion Constructors

        #region Methods

        public Order CreateOrder(ProductType product, int amount)
        {
            Order selectedOrder = _orders.FirstOrDefault(x => x.Product.Equals(product));
            if (selectedOrder != null)
            {
                selectedOrder.AddAmount(selectedOrder.Amount);
                return selectedOrder;
            }
            else
            {
                Order newOrder = new Order(product, amount, this);
                _orders.Add(newOrder);
                return newOrder;
            }
        }

        public Order ChangeOrder(Order order, ProductType product, int amount)
        {
            Order orderWithProductType = _orders.FirstOrDefault(x => x.Product.Equals(product));
            if (orderWithProductType != null)
            {
                orderWithProductType.AddAmount(amount);
                return orderWithProductType;
            }
            else
            {
                order.Amount = amount;
                order.Product = product;
                return order;
            }
        }

        public void RemoveOrder(int orderId)
        {
            Order order = Orders.FirstOrDefault(x => x.Id == orderId);
            _orders.Remove(order);
        }
        #endregion Methods
    }
}