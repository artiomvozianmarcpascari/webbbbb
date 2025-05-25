using System;
using System.Collections.Generic;
using System.Linq;
using Vintagefur.Domain.Models;

namespace Vintagefur.Infrastructure.Repositories
{
    public class CategoryRepository
    {
        private static List<Category> _categories = new List<Category>();
        
        public CategoryRepository()
        {
            // Инициализация репозитория, если нужно
            if (_categories.Count == 0)
            {
                _categories.Add(new Category { Id = 1, Name = "Мебель", Description = "Антикварная мебель" });
                _categories.Add(new Category { Id = 2, Name = "Освещение", Description = "Винтажные лампы и светильники" });
                _categories.Add(new Category { Id = 3, Name = "Декор", Description = "Винтажные предметы интерьера" });
            }
        }
        
        public Category GetById(int id)
        {
            return _categories.FirstOrDefault(c => c.Id == id);
        }
        
        public List<Category> GetAll()
        {
            return _categories.ToList();
        }
        
        public bool Create(Category category)
        {
            try
            {
                if (category.Id == 0)
                {
                    category.Id = _categories.Count > 0 ? _categories.Max(c => c.Id) + 1 : 1;
                }
                
                _categories.Add(category);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Update(Category category)
        {
            try
            {
                var existingCategory = GetById(category.Id);
                if (existingCategory == null)
                {
                    return false;
                }
                
                _categories.Remove(existingCategory);
                _categories.Add(category);
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
                var category = GetById(id);
                if (category == null)
                {
                    return false;
                }
                
                _categories.Remove(category);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 