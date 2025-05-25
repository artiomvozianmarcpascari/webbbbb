using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Data;
using Vintagefur.BusinessLogic.Interfaces;

namespace Vintagefur.BusinessLogic.Services
{
    public class OrderServiceBusinessLogic
    {
        private readonly VintagefurDbContext _dbContext;
        private readonly IOrderBL _orderBL;

        public OrderServiceBusinessLogic()
        {
            _dbContext = new VintagefurDbContext();
            _orderBL = BusinessLogicFactory.Instance.GetOrderBL();
        }

        public int CreateOrder(Customer customer, List<OrderItem> items, string shippingAddress, string shippingCity, string shippingPostalCode, string shippingCountry, string notes = null)
        {
            var order = new Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                TotalAmount = CalculateTotal(items),
                ShippingAddress = shippingAddress,
                ShippingCity = shippingCity,
                ShippingPostalCode = shippingPostalCode,
                ShippingCountry = shippingCountry,
                Notes = notes,
                OrderItems = items
            };
            
            if (_orderBL.CreateOrder(order))
            {
                return order.Id;
            }
            
            return 0;
        }

        public Order GetOrderById(int orderId)
        {
            return _orderBL.GetOrderById(orderId);
        }

        public List<Order> GetAllOrders()
        {
            return _orderBL.GetAllOrders();
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return _orderBL.GetOrdersByUserId(userId);
        }

        public bool UpdateOrder(Order order)
        {
            return _orderBL.UpdateOrder(order);
        }

        public bool DeleteOrder(int orderId)
        {
            return _orderBL.DeleteOrder(orderId);
        }

        private decimal CalculateTotal(List<OrderItem> items)
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total += item.UnitPrice * item.Quantity;
            }
            return total;
        }

        public List<Order> GetCustomerOrders(int customerId)
        {
            return _dbContext.Orders
                .Include(o => o.OrderItems.Select(i => i.Product))
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public void UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = _dbContext.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = status;
                _dbContext.SaveChanges();
            }
        }
    }
} 