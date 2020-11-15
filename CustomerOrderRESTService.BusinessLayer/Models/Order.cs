using CustomerOrderRESTService.BusinessLayer.Exceptions;
using System;

namespace CustomerOrderRESTService.BusinessLayer.Models
{
    public class Order
    {
        #region Properties

        public int Id { get; set; }
        public ProductType Product { get; set; }
        public int Amount { get; private set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

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

        internal void ChangeAmount(int amount)
        {
            if (amount <= 1) throw new BusinessException("amount must be more than 1");
            Amount = amount;
        }

        #endregion Methods
    }
}