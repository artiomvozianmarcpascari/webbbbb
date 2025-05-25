using System;
using System.Collections.Generic;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Data;

namespace Vintagefur.BusinessLogic.Core
{
    public class ProductApi
    {
        protected List<Product> GetAllProductsAction()
        {
            using (var db = new VintagefurDbContext())
            {
                return db.Products
                    .Include("Category")
                    .Include("Material")
                    .Include("ProductStyle")
                    .ToList();
            }
        }

        protected Product GetProductByIdAction(int id)
        {
            using (var db = new VintagefurDbContext())
            {
                return db.Products
                    .Include("Category")
                    .Include("Material")
                    .Include("ProductStyle")
                    .FirstOrDefault(p => p.Id == id);
            }
        }

        protected List<Product> GetProductsByCategoryAction(int categoryId)
        {
            using (var db = new VintagefurDbContext())
            {
                return db.Products
                    .Include("Category")
                    .Include("Material")
                    .Include("ProductStyle")
                    .Where(p => p.CategoryId == categoryId)
                    .ToList();
            }
        }

        protected List<Product> SearchProductsAction(string searchTerm)
        {
            using (var db = new VintagefurDbContext())
            {
                return db.Products
                    .Include("Category")
                    .Include("Material")
                    .Include("ProductStyle")
                    .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                    .ToList();
            }
        }

        protected bool CreateProductAction(Product product)
        {
            try
            {
                using (var db = new VintagefurDbContext())
                {
                    db.Products.Add(product);
                    return db.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected bool UpdateProductAction(Product product)
        {
            try
            {
                using (var db = new VintagefurDbContext())
                {
                    var existingProduct = db.Products.Find(product.Id);
                    if (existingProduct == null)
                        return false;

                    db.Entry(existingProduct).CurrentValues.SetValues(product);
                    return db.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected bool DeleteProductAction(int id)
        {
            try
            {
                using (var db = new VintagefurDbContext())
                {
                    var product = db.Products.Find(id);
                    if (product == null)
                        return false;

                    db.Products.Remove(product);
                    return db.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
} 