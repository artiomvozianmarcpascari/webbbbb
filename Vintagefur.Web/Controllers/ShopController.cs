using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vintagefur.Application.Services;
using Vintagefur.Domain.Models;

namespace Vintagefur.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductService _productService;

        public ShopController()
        {
            _productService = new ProductService();
        }

        // GET: Shop
        public ActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        // GET: Shop/Category/5
        public ActionResult Category(int id)
        {
            var products = _productService.GetProductsByCategory(id);
            return View("Index", products);
        }

        // GET: Shop/Details/5
        public ActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
} 