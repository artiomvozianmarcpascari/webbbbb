using System;
using System.Collections.Generic;
using System.Linq;
using Vintagefur.Domain.Models;

namespace Vintagefur.Infrastructure.Repositories
{
    public class OrderRepository
    {
        private static List<Order> _orders = new List<Order>();
        
        public OrderRepository()
        {
            // Инициализация репозитория, если нужно
        }
        
        public Order GetById(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }
        
        public List<Order> GetAll()
        {
            return _orders.ToList();
        }
        
        public List<Order> GetOrdersByUserId(int userId)
        {
            // Для этого метода нам нужно знать, какому пользователю принадлежит заказ
            // Для простоты предположим, что у нас есть связь через CustomerId
            var customerRepository = new CustomerRepository();
            var customer = customerRepository.GetByUserId(userId);
            
            if (customer != null)
            {
                return _orders.Where(o => o.CustomerId == customer.Id).ToList();
            }
            
            return new List<Order>();
        }
        
        public bool Create(Order order)
        {
            try
            {
                if (order.Id == 0)
                {
                    order.Id = _orders.Count > 0 ? _orders.Max(o => o.Id) + 1 : 1;
                }
                
                if (order.OrderDate == DateTime.MinValue)
                {
                    order.OrderDate = DateTime.Now;
                }
                
                _orders.Add(order);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Update(Order order)
        {
            try
            {
                var existingOrder = GetById(order.Id);
                if (existingOrder == null)
                {
                    return false;
                }
                
                _orders.Remove(existingOrder);
                _orders.Add(order);
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
                var order = GetById(id);
                if (order == null)
                {
                    return false;
                }
                
                _orders.Remove(order);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 