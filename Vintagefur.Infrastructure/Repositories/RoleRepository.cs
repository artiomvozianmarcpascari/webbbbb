using System;
using System.Collections.Generic;
using System.Linq;
using Vintagefur.Domain.Models;

namespace Vintagefur.Infrastructure.Repositories
{
    public class RoleRepository
    {
        private static List<Role> _roles = new List<Role>();
        
        public RoleRepository()
        {
            // Инициализация репозитория, если нужно
            if (_roles.Count == 0)
            {
                _roles.Add(new Role { Id = 1, Name = "Admin" });
                _roles.Add(new Role { Id = 2, Name = "User" });
            }
        }
        
        public Role GetById(int id)
        {
            return _roles.FirstOrDefault(r => r.Id == id);
        }
        
        public Role GetByName(string name)
        {
            return _roles.FirstOrDefault(r => r.Name == name);
        }
        
        public List<Role> GetAll()
        {
            return _roles.ToList();
        }
        
        public bool Create(Role role)
        {
            try
            {
                if (role.Id == 0)
                {
                    role.Id = _roles.Count > 0 ? _roles.Max(r => r.Id) + 1 : 1;
                }
                
                _roles.Add(role);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public bool Update(Role role)
        {
            try
            {
                var existingRole = GetById(role.Id);
                if (existingRole == null)
                {
                    return false;
                }
                
                _roles.Remove(existingRole);
                _roles.Add(role);
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
                var role = GetById(id);
                if (role == null)
                {
                    return false;
                }
                
                _roles.Remove(role);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
} 