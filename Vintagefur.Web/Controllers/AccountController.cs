using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Vintagefur.BusinessLogic;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.Models;

namespace Vintagefur.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserBL _userBL;

        public AccountController()
        {
            var factory = BusinessLogicFactory.Instance;
            _userBL = factory.GetUserBL();
        }

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

            var authResult = _userBL.AuthenticateUser(email, password);
            if (authResult.IsSuccess)
            {
                System.Diagnostics.Debug.WriteLine($"Успешная аутентификация: {email}");
                System.Diagnostics.Debug.WriteLine($"UserId: {authResult.UserId}");
                System.Diagnostics.Debug.WriteLine($"Role: {authResult.UserRole}");
                
                HttpContext.Response.Cookies.Add(authResult.Cookie);
                Session["UserName"] = email;
                Session["UserRole"] = authResult.UserRole;
                
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
            
            ViewBag.ErrorMessage = authResult.ErrorMessage ?? "Неверный email или пароль";
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

            // Создание пользователя через бизнес-логику
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = password // В реальном приложении здесь должно быть хеширование
            };

            if (_userBL.UpdateUser(user))
            {
                var authResult = _userBL.AuthenticateUser(email, password);
                if (authResult.IsSuccess)
                {
                    HttpContext.Response.Cookies.Add(authResult.Cookie);
                    Session["UserName"] = $"{firstName} {lastName}";
                    Session["UserRole"] = authResult.UserRole;
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.ErrorMessage = "Пользователь с таким email уже существует";
            return View();
        }

        public ActionResult Logout()
        {
            var signOutResult = _userBL.SignOut();
            if (signOutResult.IsSuccess)
            {
                HttpContext.Response.Cookies.Add(signOutResult.Cookie);
            }

            Session["UserName"] = null;
            Session["UserRole"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
} 