using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vintagefur.BusinessLogic.Services;
using Vintagefur.Domain.Models;

namespace Vintagefur.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductServiceBusinessLogic _productServiceBusinessLogic;

        public ShopController()
        {
            _productServiceBusinessLogic = new ProductServiceBusinessLogic();
        }

        // GET: Shop
        public ActionResult Index()
        {
            var products = _productServiceBusinessLogic.GetAllProducts();
            return View(products);
        }

        // GET: Shop/Category/5
        public ActionResult Category(int id)
        {
            var products = _productServiceBusinessLogic.GetProductsByCategory(id);
            return View("Index", products);
        }

        // GET: Shop/Details/5
        public ActionResult Details(int id)
        {
            var product = _productServiceBusinessLogic.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
} 