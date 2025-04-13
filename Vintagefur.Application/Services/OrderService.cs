using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Data;

namespace Vintagefur.Application.Services
{
    public class OrderService
    {
        private readonly VintagefurDbContext _dbContext;

        public OrderService()
        {
            _dbContext = new VintagefurDbContext();
        }

        public int CreateOrder(Customer customer, List<OrderItem> orderItems, string shippingAddress, 
            string shippingCity, string shippingPostalCode, string shippingCountry, string notes = null)
        {
            var order = new Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                TotalAmount = orderItems.Sum(i => i.UnitPrice * i.Quantity),
                ShippingAddress = shippingAddress,
                ShippingCity = shippingCity,
                ShippingPostalCode = shippingPostalCode,
                ShippingCountry = shippingCountry,
                Notes = notes,
                OrderItems = orderItems
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return order.Id;
        }

        public Order GetOrderById(int orderId)
        {
            return _dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems.Select(i => i.Product))
                .FirstOrDefault(o => o.Id == orderId);
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