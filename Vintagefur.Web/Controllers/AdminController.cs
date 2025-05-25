using System;
using System.Linq;
using System.Web.Mvc;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.BusinessLogic.Services;
using Vintagefur.Domain.Models;
using System.Collections.Generic;
using System.Data.Entity;
using Vintagefur.Infrastructure.Data;
using System.Diagnostics;

namespace Vintagefur.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ProductServiceBusinessLogic _productServiceBusinessLogic;
        private readonly AdminServiceBusinessLogic _adminService;
        private readonly VintagefurDbContext _dbContext;

        public AdminController()
        {
            _productServiceBusinessLogic = new ProductServiceBusinessLogic();
            _adminService = new AdminServiceBusinessLogic();
            _dbContext = new VintagefurDbContext();
            
            // Диагностика - безопасная
            Debug.WriteLine("AdminController создан");
        }

        // GET: Admin
        public ActionResult Index()
        {
            // Диагностика - безопасная
            Debug.WriteLine("Вызов AdminController.Index");
            
            // В этом методе User.Identity уже должен быть доступен
            if (User != null && User.Identity != null)
            {
                Debug.WriteLine($"Пользователь аутентифицирован: {User.Identity.IsAuthenticated}");
                if (User.Identity.IsAuthenticated)
                {
                    Debug.WriteLine($"Имя пользователя: {User.Identity.Name}");
                    Debug.WriteLine($"Роль Admin: {User.IsInRole("Admin")}");
                }
            }
            
            ViewBag.ProductCount = _adminService.GetProductCount();
            ViewBag.CategoryCount = _adminService.GetCategoryCount();
            ViewBag.OrderCount = _adminService.GetOrderCount();
            ViewBag.CustomerCount = _adminService.GetCustomerCount();
            ViewBag.PendingOrdersCount = _adminService.GetPendingOrdersCount();

            return View();
        }

        #region Products Management

        // GET: Admin/Products
        public ActionResult Products()
        {
            var products = _adminService.GetAllProducts();
            return View(products);
        }

        // GET: Admin/CreateProduct
        public ActionResult CreateProduct()
        {
            ViewBag.Categories = new SelectList(_dbContext.Categories, "Id", "Name");
            ViewBag.Materials = new SelectList(_dbContext.Materials, "Id", "Name");
            ViewBag.Styles = new SelectList(_dbContext.ProductStyles, "Id", "Name");
            return View();
        }

        // POST: Admin/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _adminService.CreateProduct(product);
                return RedirectToAction("Products");
            }

            ViewBag.Categories = new SelectList(_dbContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Materials = new SelectList(_dbContext.Materials, "Id", "Name", product.MaterialId);
            ViewBag.Styles = new SelectList(_dbContext.ProductStyles, "Id", "Name", product.StyleId);
            return View(product);
        }

        // GET: Admin/EditProduct/5
        public ActionResult EditProduct(int id)
        {
            var product = _adminService.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = new SelectList(_dbContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Materials = new SelectList(_dbContext.Materials, "Id", "Name", product.MaterialId);
            ViewBag.Styles = new SelectList(_dbContext.ProductStyles, "Id", "Name", product.StyleId);
            return View(product);
        }

        // POST: Admin/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _adminService.UpdateProduct(product);
                return RedirectToAction("Products");
            }

            ViewBag.Categories = new SelectList(_dbContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Materials = new SelectList(_dbContext.Materials, "Id", "Name", product.MaterialId);
            ViewBag.Styles = new SelectList(_dbContext.ProductStyles, "Id", "Name", product.StyleId);
            return View(product);
        }

        // GET: Admin/DeleteProduct/5
        public ActionResult DeleteProduct(int id)
        {
            var product = _adminService.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/DeleteProduct/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductConfirmed(int id)
        {
            _adminService.DeleteProduct(id);
            return RedirectToAction("Products");
        }

        #endregion

        #region Categories Management

        // GET: Admin/Categories
        public ActionResult Categories()
        {
            var categories = _adminService.GetAllCategories();
            return View(categories);
        }

        // GET: Admin/CreateCategory
        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST: Admin/CreateCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _adminService.CreateCategory(category);
                return RedirectToAction("Categories");
            }
            return View(category);
        }

        // GET: Admin/EditCategory/5
        public ActionResult EditCategory(int id)
        {
            var category = _adminService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/EditCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _adminService.UpdateCategory(category);
                return RedirectToAction("Categories");
            }
            return View(category);
        }

        // GET: Admin/DeleteCategory/5
        public ActionResult DeleteCategory(int id)
        {
            var category = _adminService.GetCategoryById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/DeleteCategory/5
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(int id)
        {
            _adminService.DeleteCategory(id);
            return RedirectToAction("Categories");
        }

        #endregion

        #region Orders Management

        // GET: Admin/Orders
        public ActionResult Orders()
        {
            var orders = _adminService.GetAllOrders();
            return View(orders);
        }

        // GET: Admin/OrderDetails/5
        public ActionResult OrderDetails(int id)
        {
            var order = _adminService.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/EditOrder/5
        public ActionResult EditOrder(int id)
        {
            var order = _adminService.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.Statuses = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "Ожидает оплаты", Value = OrderStatus.Pending.ToString() },
                    new SelectListItem { Text = "Обработан", Value = OrderStatus.Processing.ToString() },
                    new SelectListItem { Text = "Отправлен", Value = OrderStatus.Shipped.ToString() },
                    new SelectListItem { Text = "Доставлен", Value = OrderStatus.Delivered.ToString() },
                    new SelectListItem { Text = "Отменён", Value = OrderStatus.Cancelled.ToString() }
                }, "Value", "Text", order.Status.ToString());

            return View(order);
        }

        // POST: Admin/EditOrder/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                _adminService.UpdateOrder(order);
                return RedirectToAction("Orders");
            }
            
            ViewBag.Statuses = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "Ожидает оплаты", Value = OrderStatus.Pending.ToString() },
                    new SelectListItem { Text = "Обработан", Value = OrderStatus.Processing.ToString() },
                    new SelectListItem { Text = "Отправлен", Value = OrderStatus.Shipped.ToString() },
                    new SelectListItem { Text = "Доставлен", Value = OrderStatus.Delivered.ToString() },
                    new SelectListItem { Text = "Отменён", Value = OrderStatus.Cancelled.ToString() }
                }, "Value", "Text", order.Status.ToString());
                
            return View(order);
        }

        // GET: Admin/DeleteOrder/5
        public ActionResult DeleteOrder(int id)
        {
            var order = _adminService.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/DeleteOrder/5
        [HttpPost, ActionName("DeleteOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrderConfirmed(int id)
        {
            _adminService.DeleteOrder(id);
            return RedirectToAction("Orders");
        }

        #endregion

        #region Customers Management

        // GET: Admin/Customers
        public ActionResult Customers()
        {
            var customers = _adminService.GetAllCustomers();
            return View(customers);
        }

        // GET: Admin/CustomerDetails/5
        public ActionResult CustomerDetails(int id)
        {
            var customer = _adminService.GetCustomerById(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            
            var customerOrders = _dbContext.Orders
                .Where(o => o.CustomerId == id)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
                
            ViewBag.Orders = customerOrders;
            
            return View(customer);
        }

        #endregion
    }
} 