using System;
using System.Linq;
using System.Web.Mvc;
using Vintagefur.Application.Services;
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
        private readonly ProductService _productService;
        private readonly VintagefurDbContext _dbContext;

        public AdminController()
        {
            _productService = new ProductService();
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
            
            var productCount = _dbContext.Products.Count();
            var categoryCount = _dbContext.Categories.Count();
            var orderCount = _dbContext.Orders.Count();
            var customerCount = _dbContext.Customers.Count();
            var pendingOrdersCount = _dbContext.Orders.Count(o => o.Status == OrderStatus.Pending);

            ViewBag.ProductCount = productCount;
            ViewBag.CategoryCount = categoryCount;
            ViewBag.OrderCount = orderCount;
            ViewBag.CustomerCount = customerCount;
            ViewBag.PendingOrdersCount = pendingOrdersCount;

            return View();
        }

        #region Products Management

        // GET: Admin/Products
        public ActionResult Products()
        {
            var products = _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Material)
                .Include(p => p.Style)
                .ToList();
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
                product.CreatedDate = DateTime.Now;
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
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
            var product = _dbContext.Products.Find(id);
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
                _dbContext.Entry(product).State = EntityState.Modified;
                _dbContext.SaveChanges();
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
            var product = _dbContext.Products.Find(id);
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
            var product = _dbContext.Products.Find(id);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
            return RedirectToAction("Products");
        }

        #endregion

        #region Categories Management

        // GET: Admin/Categories
        public ActionResult Categories()
        {
            var categories = _dbContext.Categories.ToList();
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
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View(category);
        }

        // GET: Admin/EditCategory/5
        public ActionResult EditCategory(int id)
        {
            var category = _dbContext.Categories.Find(id);
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
                _dbContext.Entry(category).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View(category);
        }

        // GET: Admin/DeleteCategory/5
        public ActionResult DeleteCategory(int id)
        {
            var category = _dbContext.Categories.Find(id);
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
            var category = _dbContext.Categories.Find(id);
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Categories");
        }

        #endregion

        #region Orders Management

        // GET: Admin/Orders
        public ActionResult Orders()
        {
            var orders = _dbContext.Orders
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
            return View(orders);
        }

        // GET: Admin/OrderDetails/5
        public ActionResult OrderDetails(int id)
        {
            var order = _dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems.Select(i => i.Product))
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/EditOrder/5
        public ActionResult EditOrder(int id)
        {
            var order = _dbContext.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.Statuses = Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString(),
                    Selected = (v == order.Status)
                })
                .ToList();

            return View(order);
        }

        // POST: Admin/EditOrder/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                var existingOrder = _dbContext.Orders.Find(order.Id);
                if (existingOrder != null)
                {
                    existingOrder.Status = order.Status;
                    existingOrder.ShippingDate = order.ShippingDate;
                    existingOrder.DeliveryDate = order.DeliveryDate;
                    existingOrder.Notes = order.Notes;
                    _dbContext.SaveChanges();
                }
                return RedirectToAction("Orders");
            }

            ViewBag.Statuses = Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString(),
                    Selected = (v == order.Status)
                })
                .ToList();

            return View(order);
        }

        #endregion

        #region Customers Management

        // GET: Admin/Customers
        public ActionResult Customers()
        {
            var customers = _dbContext.Customers.ToList();
            return View(customers);
        }

        // GET: Admin/CustomerDetails/5
        public ActionResult CustomerDetails(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var orders = _dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.CustomerId == id)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            ViewBag.Orders = orders;
            return View(customer);
        }

        #endregion
    }
} 