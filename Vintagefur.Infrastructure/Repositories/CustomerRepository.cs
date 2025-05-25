using System.Collections.Generic;
using System.Linq;
using Vintagefur.Domain.Models;

namespace Vintagefur.Infrastructure.Repositories
{
    public class CustomerRepository
    {
        private static List<Customer> _customers = new List<Customer>();
        
        public CustomerRepository()
        {
            // Инициализация репозитория, если нужно
        }
        
        public Customer GetById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }
        
        public Customer GetByUserId(int userId)
        {
            return _customers.FirstOrDefault(c => c.UserId == userId);
        }
        
        public List<Customer> GetAll()
        {
            return _customers.ToList();
        }
        
        public bool Create(Customer customer)
        {
            try
            {
                if (customer.Id == 0)
                {
                    customer.Id = _customers.Count > 0 ? _customers.Max(c => c.Id) + 1 : 1;
                }
                
                _customers.Add(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Update(Customer customer)
        {
            try
            {
                var existingCustomer = GetById(customer.Id);
                if (existingCustomer == null)
                {
                    return false;
                }
                
                _customers.Remove(existingCustomer);
                _customers.Add(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Delete(int id)
        {
            try
            {
                var customer = GetById(id);
                if (customer == null)
                {
                    return false;
                }
                
                _customers.Remove(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 