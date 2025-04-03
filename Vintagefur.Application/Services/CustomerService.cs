using System;
using System.Linq;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Data;

namespace Vintagefur.Application.Services
{
    public class CustomerService
    {
        private readonly VintagefurDbContext _dbContext;

        public CustomerService()
        {
            _dbContext = new VintagefurDbContext();
        }

        public int CreateCustomer(string firstName, string lastName, string email, 
            string address = null, string city = null, string postalCode = null, string country = null)
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

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return customer.Id;
        }

        public Customer GetCustomerById(int id)
        {
            return _dbContext.Customers.Find(id);
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Email == email);
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = _dbContext.Customers.Find(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.Address = customer.Address;
                existingCustomer.City = customer.City;
                existingCustomer.PostalCode = customer.PostalCode;
                existingCustomer.Country = customer.Country;

                _dbContext.SaveChanges();
            }
        }
    }
} 