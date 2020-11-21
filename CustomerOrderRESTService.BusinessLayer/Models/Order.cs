using CustomerOrderRESTService.BusinessLayer.Exceptions;
using System;

namespace CustomerOrderRESTService.BusinessLayer.Models
{
    public class Order
    {
        #region Fields
        private int _id;
        private int _amount;
        private Customer _customer;
        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= -1) throw new BusinessException("id can't be lower than 0");
                _id = value;
            }
        }
        public ProductType Product { get; set; }
        public int Amount {
            get { return _amount; }
            internal set
            {
                if (value < 1) throw new BusinessException("amount must be more than 1");
                _amount = value;
            }
        }
        public Customer Customer {
            get { return _customer; }
            internal set
            {
                if (value == null) throw new ArgumentNullException("customer can not be null");
                _customer = value;
            }
        }

        #endregion Properties

        #region Constructors

        internal Order()
        {
        }

        internal Order(ProductType product, int amount, Customer customer)
        {
            Product = product;
            if (amount <= 1) throw new BusinessException("amount must be more than 1");
            Amount = amount;
            Customer = customer ?? throw new NullReferenceException(nameof(customer));
        }

        #endregion Constructors

        #region Methods

        internal void AddAmount(int amount)
        {
            if (amount <= 1) throw new BusinessException("amount must be more than 1");
            Amount += amount;
        }

        internal void ChangeCustomer(Customer newCustomer)
        {
            Customer.RemoveOrder(this.Id);
            Customer = newCustomer;
            newCustomer.CreateOrder(this.Product, this.Amount);
        }
        #endregion Methods
    }
}