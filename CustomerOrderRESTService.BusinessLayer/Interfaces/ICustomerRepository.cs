﻿using CustomerOrderRESTService.BusinessLayer.Models;

namespace CustomerOrderRESTService.BusinessLayer.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);

        void UpdateCustomer(int id, string name, string address, string combo);

        void RemoveCustomer(int id);

        Customer Find(int id);

        Customer Find(string uniqueNameAddressCombo);
    }
}