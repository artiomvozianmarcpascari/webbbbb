using System;
using System.Collections.Generic;
using System.Linq;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Data;

namespace Vintagefur.Infrastructure.Repositories
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>();
        
        public ProductRepository()
        {
            // Инициализация репозитория, если нужно
        }
        
        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
        
        public List<Product> GetAll()
        {
            return _products.ToList();
        }
        
        public List<Product> GetByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }
        
        public bool Create(Product product)
        {
            try
            {
                if (product.Id == 0)
                {
                    product.Id = _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1;
                }
                
                product.CreatedDate = DateTime.Now;
                _products.Add(product);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Update(Product product)
        {
            try
            {
                var existingProduct = GetById(product.Id);
                if (existingProduct == null)
                {
                    return false;
                }
                
                _products.Remove(existingProduct);
                _products.Add(product);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Delete(int id)
        {
            try
            {
                var product = GetById(id);
                if (product == null)
                {
                    return false;
                }
                
                _products.Remove(product);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 