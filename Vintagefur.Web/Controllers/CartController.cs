using System;
using System.Web.Mvc;
using Vintagefur.Application.Services;
using Vintagefur.Web.Models;

namespace Vintagefur.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductService _productService;

        public CartController()
        {
            _productService = new ProductService();
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            return View(cart);
        }

        // POST: Cart/AddToCart/5
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var cart = ShoppingCart.GetCart(HttpContext);
            cart.AddItem(product, quantity);
            cart.SaveToSession(HttpContext);

            // After adding to cart, redirect back to the product page
            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }

            return RedirectToAction("Index");
        }

        // POST: Cart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            cart.RemoveItem(id);
            cart.SaveToSession(HttpContext);

            return RedirectToAction("Index");
        }

        // POST: Cart/UpdateQuantity/5?quantity=3
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            cart.UpdateQuantity(id, quantity);
            cart.SaveToSession(HttpContext);

            return RedirectToAction("Index");
        }

        // POST: Cart/ClearCart
        [HttpPost]
        public ActionResult ClearCart()
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            cart.Clear();
            cart.SaveToSession(HttpContext);

            return RedirectToAction("Index");
        }

        // AJAX: Cart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            ViewBag.CartCount = cart.TotalQuantity;
            return PartialView("_CartSummary");
        }
    }
} 