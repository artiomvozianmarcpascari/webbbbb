using System;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Core
{
    public class AuthApi
    {
        public User AuthenticateUser(string email, string password)
        {
            // Базовая реализация
            return new User { Email = email };
        }

        public User CreateAccount(string firstName, string lastName, string email, string password)
        {
            // Базовая реализация
            return new User 
            { 
                FirstName = firstName, 
                LastName = lastName, 
                Email = email 
            };
        }

        public Role GetRoleById(int roleId)
        {
            // Базовая реализация
            return new Role { Id = roleId, Name = "User" };
        }

        public bool IsEmailAvailable(string email)
        {
            // Базовая реализация
            return true;
        }
    }
} 