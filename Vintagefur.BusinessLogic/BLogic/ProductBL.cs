using System;
using System.Collections.Generic;
using System.Linq;
using Vintagefur.BusinessLogic.Core;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.DTO;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.BLogic
{
    internal class ProductBL : ProductApi, IProduct
    {
        public List<ProductDTO> GetAllProducts()
        {
            // Вызов метода базового класса
            var products = GetAllProductsAction();
            
            // Преобразование и бизнес-логика
            return products.Select(p => MapToDTO(p)).ToList();
        }

        public ProductDTO GetProductById(int id)
        {
            // Вызов метода базового класса
            var product = GetProductByIdAction(id);
            
            // Преобразование и бизнес-логика
            return product != null ? MapToDTO(product) : null;
        }

        public List<ProductDTO> GetProductsByCategory(int categoryId)
        {
            // Вызов метода базового класса
            var products = GetProductsByCategoryAction(categoryId);
            
            // Преобразование и бизнес-логика
            return products.Select(p => MapToDTO(p)).ToList();
        }

        public List<ProductDTO> SearchProducts(string searchTerm)
        {
            // Валидация входных данных
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<ProductDTO>();
                
            // Вызов метода базового класса
            var products = SearchProductsAction(searchTerm);
            
            // Преобразование и бизнес-логика
            return products.Select(p => MapToDTO(p)).ToList();
        }

        public bool CreateProduct(ProductDTO productDTO)
        {
            // Валидация входных данных
            if (productDTO == null || string.IsNullOrWhiteSpace(productDTO.Name))
                return false;
                
            // Преобразование DTO в доменную модель
            var product = MapToEntity(productDTO);
            
            // Вызов метода базового класса
            return CreateProductAction(product);
        }

        public bool UpdateProduct(ProductDTO productDTO)
        {
            // Валидация входных данных
            if (productDTO == null || productDTO.Id <= 0)
                return false;
                
            // Преобразование DTO в доменную модель
            var product = MapToEntity(productDTO);
            
            // Вызов метода базового класса
            return UpdateProductAction(product);
        }

        public bool DeleteProduct(int id)
        {
            // Валидация входных данных
            if (id <= 0)
                return false;
                
            // Вызов метода базового класса
            return DeleteProductAction(id);
        }

        // Вспомогательные методы для преобразования между DTO и доменными объектами
        private ProductDTO MapToDTO(Product product)
        {
            if (product == null)
                return null;

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                IsAvailable = product.IsAvailable,
                MaterialId = product.MaterialId,
                MaterialName = product.Material?.Name,
                StyleId = product.StyleId,
                StyleName = product.Style?.Name
            };
        }

        private Product MapToEntity(ProductDTO dto)
        {
            if (dto == null)
                return null;

            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId,
                IsAvailable = dto.IsAvailable,
                MaterialId = dto.MaterialId,
                StyleId = dto.StyleId
            };
        }
    }
} 