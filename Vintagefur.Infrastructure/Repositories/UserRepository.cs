using System;
using System.Collections.Generic;
using System.Linq;
using Vintagefur.Domain.Models;

namespace Vintagefur.Infrastructure.Repositories
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>();
        
        public UserRepository()
        {
            // Инициализация репозитория, если нужно
            if (_users.Count == 0)
            {
                _users.Add(new User 
                { 
                    Id = 1, 
                    Email = "admin@example.com", 
                    FirstName = "Admin", 
                    LastName = "User",
                    PasswordHash = "admin123",
                    RegistrationDate = DateTime.Now,
                    IsActive = true,
                    RoleId = 1,
                    Phone = "123-456-7890",
                    Address = "123 Main St"
                });
            }
        }
        
        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
        
        public User GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }
        
        public List<User> GetAll()
        {
            return _users.ToList();
        }
        
        public bool Create(User user)
        {
            try
            {
                if (user.Id == 0)
                {
                    user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
                }
                
                _users.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Update(User user)
        {
            try
            {
                var existingUser = GetById(user.Id);
                if (existingUser == null)
                {
                    return false;
                }
                
                _users.Remove(existingUser);
                _users.Add(user);
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
                var user = GetById(id);
                if (user == null)
                {
                    return false;
                }
                
                _users.Remove(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 