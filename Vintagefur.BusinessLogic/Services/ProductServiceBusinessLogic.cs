using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Data;

namespace Vintagefur.BusinessLogic.Services
{
    public class ProductServiceBusinessLogic
    {
        private readonly VintagefurDbContext _dbContext;

        public ProductServiceBusinessLogic()
        {
            _dbContext = new VintagefurDbContext();
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Material)
                .Include(p => p.Style)
                .Where(p => p.IsAvailable)
                .ToList();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Material)
                .Include(p => p.Style)
                .Where(p => p.CategoryId == categoryId && p.IsAvailable)
                .ToList();
        }

        public Product GetProductById(int id)
        {
            return _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Material)
                .Include(p => p.Style)
                .Include(p => p.Feedbacks)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Category> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }
    }
} 