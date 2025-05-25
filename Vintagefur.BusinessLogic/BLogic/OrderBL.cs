using System;
using System.Collections.Generic;
using System.Linq;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Repositories;

namespace Vintagefur.BusinessLogic.BLogic
{
    public class OrderBL : IOrderBL
    {
        private readonly OrderRepository _orderRepository;
        private readonly UserRepository _userRepository;

        public OrderBL()
        {
            _orderRepository = new OrderRepository();
            _userRepository = new UserRepository();
        }

        public Order GetOrderById(int orderId)
        {
            return _orderRepository.GetById(orderId);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return _orderRepository.GetOrdersByUserId(userId);
        }

        public List<Order> GetOrdersByUserId(Guid userId)
        {
            // Находим пользователя по Guid (это может быть, например, внешний ID или временный ID гостя)
            var user = _userRepository.GetAll().FirstOrDefault(u => u.ExternalId == userId);
            if (user != null)
            {
                return GetOrdersByUserId(user.Id);
            }
            return new List<Order>();
        }

        public bool CreateOrder(Order order)
        {
            return _orderRepository.Create(order);
        }

        public bool UpdateOrder(Order order)
        {
            return _orderRepository.Update(order);
        }

        public bool DeleteOrder(int orderId)
        {
            return _orderRepository.Delete(orderId);
        }
    }
} 