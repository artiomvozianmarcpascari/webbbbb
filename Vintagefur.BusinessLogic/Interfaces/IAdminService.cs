using System;
using System.Collections.Generic;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        // Dashboard
        int GetProductCount();
        int GetCategoryCount();
        int GetOrderCount();
        int GetCustomerCount();
        int GetPendingOrdersCount();

        // Products
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int productId);

        // Categories
        List<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(int categoryId);

        // Orders
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        List<Order> GetOrdersByCustomerId(int customerId);

        // Customers
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);

        // Materials
        List<Material> GetAllMaterials();
        Material GetMaterialById(int id);

        // Styles
        List<ProductStyle> GetAllStyles();
        ProductStyle GetStyleById(int id);

        bool DeleteOrderItem(int orderItemId);
    }
} 