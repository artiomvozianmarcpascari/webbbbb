using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Vintagefur.Application.Services;
using Vintagefur.Domain.Models;

namespace Vintagefur.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService = new AuthService();

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password, string returnUrl)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Введите email и пароль";
                return View();
            }

            var user = _authService.AuthenticateUser(email, password);
            if (user != null)
            {
                System.Diagnostics.Debug.WriteLine($"Успешная аутентификация: {email}");
                System.Diagnostics.Debug.WriteLine($"RoleId: {user.RoleId}");
                System.Diagnostics.Debug.WriteLine($"Role: {(user.Role != null ? user.Role.Name : "null")}");
                
                AuthenticateUser(user);
                
                System.Diagnostics.Debug.WriteLine($"Session после аутентификации:");
                System.Diagnostics.Debug.WriteLine($"UserName: {Session["UserName"]}");
                System.Diagnostics.Debug.WriteLine($"UserRole: {Session["UserRole"]}");
                
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    System.Diagnostics.Debug.WriteLine($"Перенаправление на: {returnUrl}");
                    return Redirect(returnUrl);
                }
                
                System.Diagnostics.Debug.WriteLine("Перенаправление на главную страницу");
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.ErrorMessage = "Неверный email или пароль";
            return View();
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || 
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || 
                string.IsNullOrEmpty(confirmPassword))
            {
                ViewBag.ErrorMessage = "Заполните все поля";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.ErrorMessage = "Пароли не совпадают";
                return View();
            }

            var user = _authService.CreateAccount(firstName, lastName, email, password);
            if (user != null)
            {
                AuthenticateUser(user);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Пользователь с таким email уже существует";
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            HttpContext.Response.Cookies.Add(authCookie);

            Session["UserName"] = null;
            Session["UserRole"] = null;

            return RedirectToAction("Index", "Home");
        }

        private void AuthenticateUser(User user)
        {
            string roleName = "User"; 
            
            if (user.Role != null)
            {
                roleName = user.Role.Name;
                System.Diagnostics.Debug.WriteLine($"User role found: {roleName}");
            }
            else if (user.RoleId.HasValue)
            {
                var role = new AuthService().GetRoleById(user.RoleId.Value);
                if (role != null)
                {
                    roleName = role.Name;
                    System.Diagnostics.Debug.WriteLine($"User role loaded manually: {roleName}");
                }
            }
            
            var ticket = new FormsAuthenticationTicket(
                1, // версия билета
                user.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30), // время истечения
                true, // постоянный куки
                roleName, // пользовательские данные
                "/");

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(cookie);
            
            Session["UserName"] = $"{user.FirstName} {user.LastName}";
            Session["UserRole"] = roleName;
        }
    }
} 