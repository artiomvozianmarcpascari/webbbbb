using System;
using System.Web;
using System.Web.Security;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.DTO;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Repositories;

namespace Vintagefur.BusinessLogic.BLogic
{
    public class AuthBL : IAuthService
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;

        public AuthBL()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
        }

        public AuthResultDto Login(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return new AuthResultDto { IsSuccess = false, ErrorMessage = "Invalid email or password" };
            }

            var roleName = "User";
            if (user.Role != null)
            {
                roleName = user.Role.Name;
            }
            else if (user.RoleId.HasValue)
            {
                var role = _roleRepository.GetById(user.RoleId.Value);
                if (role != null)
                {
                    roleName = role.Name;
                }
            }

            var ticket = new FormsAuthenticationTicket(
                1,
                user.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                true,
                roleName,
                "/"
            );

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            return new AuthResultDto
            {
                IsSuccess = true,
                UserId = user.Id,
                Email = user.Email,
                UserRole = roleName,
                Cookie = cookie
            };
        }

        public SignOutResultDto Logout()
        {
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };

            return new SignOutResultDto
            {
                IsSuccess = true,
                Cookie = authCookie
            };
        }

        public bool Register(string email, string password, string firstName, string lastName)
        {
            if (_userRepository.GetByEmail(email) != null)
            {
                return false; // Пользователь с таким email уже существует
            }

            var user = new User
            {
                Email = email,
                PasswordHash = HashPassword(password),
                FirstName = firstName,
                LastName = lastName,
                RegistrationDate = DateTime.Now,
                IsActive = true,
                RoleId = 2 // Обычный пользователь
            };

            return _userRepository.Create(user);
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            // В реальном приложении здесь должна быть проверка хеша пароля
            // Это упрощенная реализация для примера
            return password == passwordHash;
        }

        private string HashPassword(string password)
        {
            // В реальном приложении здесь должно быть хеширование пароля
            // Это упрощенная реализация для примера
            return password;
        }

        public User AuthenticateUser(string email, string password)
        {
            // Здесь должен быть код для аутентификации пользователя
            var user = _userRepository.GetByEmail(email);
            if (user != null && user.PasswordHash == password) // В реальном приложении здесь должна быть проверка хеша пароля
            {
                return user;
            }
            return null;
        }

        public User CreateAccount(string firstName, string lastName, string email, string password)
        {
            // Проверяем, существует ли пользователь с таким email
            if (!IsEmailAvailable(email))
            {
                return null;
            }

            // Создаем нового пользователя
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = password, // В реальном приложении здесь должно быть хеширование пароля
                RoleId = 2 // Роль "User" по умолчанию
            };

            // Сохраняем пользователя в базу данных
            _userRepository.Update(user);

            return user;
        }

        public Role GetRoleById(int roleId)
        {
            return _roleRepository.GetById(roleId);
        }

        public bool IsEmailAvailable(string email)
        {
            // Проверяем, существует ли пользователь с таким email
            var user = _userRepository.GetByEmail(email);
            return user == null;
        }
    }
} 