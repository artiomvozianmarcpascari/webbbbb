using System;
using System.Collections.Generic;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Services
{
    public class CustomerServiceBusinessLogic
    {
        private readonly ICustomerBL _customerBL;
        
        public CustomerServiceBusinessLogic()
        {
            _customerBL = BusinessLogicFactory.Instance.GetCustomerBL();
        }
        
        public Customer GetCustomerById(int customerId)
        {
            return _customerBL.GetCustomerById(customerId);
        }
        
        public Customer GetCustomerByUserId(int userId)
        {
            return _customerBL.GetCustomerByUserId(userId);
        }
        
        public Customer GetCustomerByEmail(string email)
        {
            // Дополнительная логика для поиска по email, может потребоваться реализация в CustomerBL
            return null;
        }
        
        public List<Customer> GetAllCustomers()
        {
            return _customerBL.GetAllCustomers();
        }
        
        public int CreateCustomer(string firstName, string lastName, string email, string address, string city, string postalCode, string country)
        {
            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Address = address,
                City = city,
                PostalCode = postalCode,
                Country = country,
                RegistrationDate = DateTime.Now
            };
            
            if (_customerBL.CreateCustomer(customer))
            {
                return customer.Id;
            }
            
            return 0;
        }
        
        public bool UpdateCustomer(Customer customer)
        {
            return _customerBL.UpdateCustomer(customer);
        }
        
        public bool DeleteCustomer(int customerId)
        {
            return _customerBL.DeleteCustomer(customerId);
        }
    }
} 