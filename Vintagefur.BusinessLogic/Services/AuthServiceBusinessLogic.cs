using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Data;

// Корректное пространство имен для Include

namespace Vintagefur.BusinessLogic.Services
{
    public class AuthServiceBusinessLogic
    {
        private readonly VintagefurDbContext _context;

        public AuthServiceBusinessLogic()
        {
            _context = new VintagefurDbContext();
        }

        public User AuthenticateUser(string email, string password)
        {
            System.Diagnostics.Debug.WriteLine($"Попытка аутентификации: {email}");
            
            // Явно загружаем пользователя с его ролью
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == email && u.IsActive);
                
            if (user == null)
            {
                System.Diagnostics.Debug.WriteLine("Пользователь не найден");
                return null;
            }
            
            System.Diagnostics.Debug.WriteLine($"Пользователь найден: {user.FirstName} {user.LastName}");
            System.Diagnostics.Debug.WriteLine($"RoleId: {user.RoleId}");
            
            // Если роль не загружена, пробуем загрузить
            if (user.Role == null && user.RoleId.HasValue)
            {
                System.Diagnostics.Debug.WriteLine($"Роль не загружена автоматически, пробуем загрузить по ID {user.RoleId}");
                user.Role = _context.Roles.Find(user.RoleId.Value);
                System.Diagnostics.Debug.WriteLine($"Найдена роль: {(user.Role != null ? user.Role.Name : "null")}");
            }

            var passwordHash = ComputeHash(password, user.Salt);
            if (passwordHash != user.PasswordHash)
            {
                System.Diagnostics.Debug.WriteLine("Неверный пароль");
                return null;
            }

            System.Diagnostics.Debug.WriteLine("Аутентификация успешна");
            return user;
        }

        public User CreateAccount(string firstName, string lastName, string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
                return null;

            var salt = GenerateSalt();
            var passwordHash = ComputeHash(password, salt);

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = passwordHash,
                Salt = salt,
                RegistrationDate = DateTime.Now,
                IsActive = true
            };

            // Проверяем, есть ли роль "User"
            var userRole = _context.Roles.FirstOrDefault(r => r.Name == "User");
            if (userRole == null)
            {
                // Если роли "User" нет, создаем её
                userRole = new Role { Name = "User", Description = "Обычный пользователь" };
                _context.Roles.Add(userRole);
                _context.SaveChanges();
            }

            user.RoleId = userRole.Id;
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        private string GenerateSalt()
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new byte[32];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        private string ComputeHash(string password, string salt)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt))
                return null;

            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", password, salt);
                var saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
            }
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }
    }
} 