using System;
using System.Collections.Generic;
using System.Linq;
using Vintagefur.BusinessLogic.Core;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Repositories;

namespace Vintagefur.BusinessLogic.BLogic
{
    public class AdminBL : AdminApi, IAdminService
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly OrderRepository _orderRepository;

        public AdminBL()
        {
            _productRepository = new ProductRepository();
            _categoryRepository = new CategoryRepository();
            _customerRepository = new CustomerRepository();
            _orderRepository = new OrderRepository();
        }
        
        // Dashboard методы
        public int GetProductCount()
        {
            var products = _productRepository.GetAll();
            return products.Count;
        }
        
        public int GetCategoryCount()
        {
            var categories = _categoryRepository.GetAll();
            return categories.Count;
        }
        
        public int GetOrderCount()
        {
            var orders = _orderRepository.GetAll();
            return orders.Count;
        }
        
        public int GetCustomerCount()
        {
            var customers = _customerRepository.GetAll();
            return customers.Count;
        }
        
        public int GetPendingOrdersCount()
        {
            var orders = _orderRepository.GetAll();
            return orders.Count(o => o.Status == OrderStatus.Pending);
        }

        // Products методы
        public bool CreateProduct(Product product)
        {
            // Вызов метода базового класса
            return base.CreateProduct(product);
        }

        public bool UpdateProduct(Product product)
        {
            // Вызов метода базового класса
            return base.UpdateProduct(product);
        }

        public bool DeleteProduct(int productId)
        {
            // Вызов метода базового класса
            return base.DeleteProduct(productId);
        }
        
        // Categories методы
        public bool CreateCategory(Category category)
        {
            // Вызов метода базового класса
            return base.CreateCategory(category);
        }

        public bool UpdateCategory(Category category)
        {
            // Вызов метода базового класса
            return base.UpdateCategory(category);
        }

        public bool DeleteCategory(int categoryId)
        {
            // Вызов метода базового класса
            return base.DeleteCategory(categoryId);
        }
        
        public List<Category> GetAllCategories()
        {
            // Здесь должна быть логика получения всех категорий
            return _categoryRepository.GetAll();
        }
        
        public Category GetCategoryById(int categoryId)
        {
            // Здесь должна быть логика получения категории по ID
            return _categoryRepository.GetById(categoryId);
        }
        
        // Orders методы
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }
        
        public Order GetOrderById(int id)
        {
            return _orderRepository.GetById(id);
        }
        
        public void UpdateOrder(Order order)
        {
            _orderRepository.Update(order);
        }
        
        public void DeleteOrder(int id)
        {
            _orderRepository.Delete(id);
        }
        
        // Customers методы
        public List<Customer> GetAllCustomers()
        {
            // Здесь должна быть логика получения всех клиентов
            return _customerRepository.GetAll();
        }
        
        public Customer GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }
        
        public bool DeleteOrderItem(int orderItemId)
        {
            // Здесь должна быть логика удаления элемента заказа
            return true;
        }
        
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }
        
        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public List<Order> GetOrdersByCustomerId(int customerId)
        {
            return _orderRepository.GetAll().Where(o => o.CustomerId == customerId).ToList();
        }
        
        public List<Material> GetAllMaterials()
        {
            // Временная реализация
            return new List<Material>();
        }
        
        public Material GetMaterialById(int id)
        {
            // Временная реализация
            return new Material { Id = id };
        }
        
        public List<ProductStyle> GetAllStyles()
        {
            // Временная реализация
            return new List<ProductStyle>();
        }
        
        public ProductStyle GetStyleById(int id)
        {
            // Временная реализация
            return new ProductStyle { Id = id };
        }
    }
} 