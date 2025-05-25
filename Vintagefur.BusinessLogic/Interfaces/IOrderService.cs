using System.Collections.Generic;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        Order GetOrderById(int orderId);
        List<Order> GetAllOrders();
        List<Order> GetOrdersByUserId(int userId);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(int orderId);
    }
} 