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
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);

        // Categories
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);

        // Orders
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);

        // Customers
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
    }
} 