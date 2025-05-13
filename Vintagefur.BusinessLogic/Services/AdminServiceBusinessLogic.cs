using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Data;

namespace Vintagefur.BusinessLogic.Services
{
    public class AdminServiceBusinessLogic : IAdminService
    {
        private readonly VintagefurDbContext _dbContext;

        public AdminServiceBusinessLogic()
        {
            _dbContext = new VintagefurDbContext();
        }

        #region Dashboard

        public int GetProductCount()
        {
            return _dbContext.Products.Count();
        }

        public int GetCategoryCount()
        {
            return _dbContext.Categories.Count();
        }

        public int GetOrderCount()
        {
            return _dbContext.Orders.Count();
        }

        public int GetCustomerCount()
        {
            return _dbContext.Customers.Count();
        }

        public int GetPendingOrdersCount()
        {
            return _dbContext.Orders.Count(o => o.Status == OrderStatus.Pending);
        }

        #endregion

        #region Products

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Material)
                .Include(p => p.Style)
                .ToList();
        }

        public Product GetProductById(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public void CreateProduct(Product product)
        {
            product.CreatedDate = DateTime.Now;
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }

        #endregion

        #region Categories

        public List<Category> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _dbContext.Categories.Find(id);
        }

        public void CreateCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
            }
        }

        #endregion

        #region Orders

        public List<Order> GetAllOrders()
        {
            return _dbContext.Orders
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems.Select(oi => oi.Product))
                .FirstOrDefault(o => o.Id == id);
        }

        public void UpdateOrder(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var order = _dbContext.Orders.Find(id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                _dbContext.SaveChanges();
            }
        }

        #endregion

        #region Customers

        public List<Customer> GetAllCustomers()
        {
            return _dbContext.Customers.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _dbContext.Customers.Find(id);
        }

        #endregion
    }
} 