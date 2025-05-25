using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Data;

namespace Vintagefur.BusinessLogic.Services
{
    public class AdminServiceBusinessLogic
    {
        private readonly IAdminService _adminService;
        
        public AdminServiceBusinessLogic()
        {
            _adminService = BusinessLogicFactory.Instance.GetAdminBL();
        }
        
        // Dashboard
        public int GetProductCount()
        {
            return _adminService.GetProductCount();
        }
        
        public int GetCategoryCount()
        {
            return _adminService.GetCategoryCount();
        }
        
        public int GetOrderCount()
        {
            return _adminService.GetOrderCount();
        }
        
        public int GetCustomerCount()
        {
            return _adminService.GetCustomerCount();
        }
        
        public int GetPendingOrdersCount()
        {
            return _adminService.GetPendingOrdersCount();
        }
        
        // Products
        public List<Product> GetAllProducts()
        {
            return _adminService.GetAllProducts();
        }
        
        public Product GetProductById(int id)
        {
            return _adminService.GetProductById(id);
        }
        
        public bool CreateProduct(Product product)
        {
            return _adminService.CreateProduct(product);
        }
        
        public bool UpdateProduct(Product product)
        {
            return _adminService.UpdateProduct(product);
        }
        
        public bool DeleteProduct(int id)
        {
            return _adminService.DeleteProduct(id);
        }
        
        // Categories
        public List<Category> GetAllCategories()
        {
            return _adminService.GetAllCategories();
        }
        
        public Category GetCategoryById(int id)
        {
            return _adminService.GetCategoryById(id);
        }
        
        public bool CreateCategory(Category category)
        {
            return _adminService.CreateCategory(category);
        }
        
        public bool UpdateCategory(Category category)
        {
            return _adminService.UpdateCategory(category);
        }
        
        public bool DeleteCategory(int id)
        {
            return _adminService.DeleteCategory(id);
        }
        
        // Orders
        public List<Order> GetAllOrders()
        {
            return _adminService.GetAllOrders();
        }
        
        public Order GetOrderById(int id)
        {
            return _adminService.GetOrderById(id);
        }
        
        public void UpdateOrder(Order order)
        {
            _adminService.UpdateOrder(order);
        }
        
        public void DeleteOrder(int id)
        {
            _adminService.DeleteOrder(id);
        }
        
        // Customers
        public List<Customer> GetAllCustomers()
        {
            return _adminService.GetAllCustomers();
        }
        
        public Customer GetCustomerById(int id)
        {
            return _adminService.GetCustomerById(id);
        }
        
        public bool DeleteOrderItem(int orderItemId)
        {
            return _adminService.DeleteOrderItem(orderItemId);
        }
    }
} 