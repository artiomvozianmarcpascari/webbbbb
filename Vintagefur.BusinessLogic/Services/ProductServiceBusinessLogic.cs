using System;
using System.Collections.Generic;
using System.Linq;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.DTO;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Services
{
    public class ProductServiceBusinessLogic
    {
        private readonly IProduct _productBL;

        public ProductServiceBusinessLogic()
        {
            _productBL = BusinessLogicFactory.Instance.GetProductBL();
        }

        public List<Product> GetAllProducts()
        {
            var productsDTO = _productBL.GetAllProducts();
            var products = new List<Product>();
            
            foreach (var productDTO in productsDTO)
            {
                products.Add(MapFromDTO(productDTO));
            }
            
            return products;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            var productsDTO = _productBL.GetProductsByCategory(categoryId);
            var products = new List<Product>();
            
            foreach (var productDTO in productsDTO)
            {
                products.Add(MapFromDTO(productDTO));
            }
            
            return products;
        }

        public Product GetProductById(int productId)
        {
            var productDTO = _productBL.GetProductById(productId);
            if (productDTO == null)
                return null;
                
            // Конвертация из DTO в сущность
            return MapFromDTO(productDTO);
        }

        public List<Category> GetAllCategories()
        {
            // Здесь должен быть метод для получения всех категорий из IProduct или CategoryRepository
            // Временная реализация
            return new List<Category>();
        }

        public List<Material> GetAllMaterials()
        {
            // Здесь должен быть метод для получения всех материалов
            // Временная реализация
            return new List<Material>();
        }
        
        public List<ProductStyle> GetAllStyles()
        {
            // Здесь должен быть метод для получения всех стилей
            // Временная реализация
            return new List<ProductStyle>();
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            var productsDTO = _productBL.SearchProducts(searchTerm);
            var products = new List<Product>();
            
            foreach (var productDTO in productsDTO)
            {
                products.Add(MapFromDTO(productDTO));
            }
            
            return products;
        }

        public bool CreateProduct(Product product)
        {
            var productDTO = MapToDTO(product);
            return _productBL.CreateProduct(productDTO);
        }

        public bool UpdateProduct(Product product)
        {
            var productDTO = MapToDTO(product);
            return _productBL.UpdateProduct(productDTO);
        }

        public bool DeleteProduct(int productId)
        {
            return _productBL.DeleteProduct(productId);
        }

        // Вспомогательные методы для конвертации между DTO и сущностями
        private Product MapFromDTO(ProductDTO productDTO)
        {
            return new Product
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                ImageUrl = productDTO.ImageUrl,
                CategoryId = productDTO.CategoryId,
                IsAvailable = productDTO.IsAvailable
            };
        }
        
        private ProductDTO MapToDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                IsAvailable = product.IsAvailable
            };
        }
    }
} 