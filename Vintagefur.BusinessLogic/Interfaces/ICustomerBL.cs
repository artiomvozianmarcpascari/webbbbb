using System;
using System.Collections.Generic;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Interfaces
{
    public interface ICustomerBL
    {
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByUserId(int userId);
        List<Customer> GetAllCustomers();
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
    }
} 