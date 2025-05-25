using System.Collections.Generic;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Repositories;

namespace Vintagefur.BusinessLogic.BLogic
{
    public class CustomerBL : ICustomerBL
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerBL()
        {
            _customerRepository = new CustomerRepository();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _customerRepository.GetById(customerId);
        }

        public Customer GetCustomerByUserId(int userId)
        {
            return _customerRepository.GetByUserId(userId);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public bool CreateCustomer(Customer customer)
        {
            return _customerRepository.Create(customer);
        }

        public bool UpdateCustomer(Customer customer)
        {
            return _customerRepository.Update(customer);
        }

        public bool DeleteCustomer(int customerId)
        {
            return _customerRepository.Delete(customerId);
        }
    }
} 