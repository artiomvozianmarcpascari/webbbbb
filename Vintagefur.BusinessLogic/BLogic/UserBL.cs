using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.DTO;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Repositories;

namespace Vintagefur.BusinessLogic.BLogic
{
    public class UserBL : IUserBL
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;

        public UserBL()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetById(userId);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public bool CreateUser(User user)
        {
            return _userRepository.Create(user);
        }

        public bool UpdateUser(User user)
        {
            return _userRepository.Update(user);
        }

        public bool DeleteUser(int userId)
        {
            return _userRepository.Delete(userId);
        }

        public AuthResultDto AuthenticateUser(string email, string password)
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

        public UserProfileDto GetUserProfile(int userId)
        {
            var user = GetUserById(userId);
            if (user == null)
            {
                return null;
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

            return new UserProfileDto
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Address = user.Address,
                Role = roleName
            };
        }

        public SignOutResultDto SignOut()
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

        private bool VerifyPassword(string password, string passwordHash)
        {
            // В реальном приложении здесь должна быть проверка хеша пароля
            // Это упрощенная реализация для примера
            return password == passwordHash;
        }
    }
} 