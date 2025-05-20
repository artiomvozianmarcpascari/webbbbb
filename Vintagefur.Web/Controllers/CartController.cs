using System;
using System.Web.Mvc;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.BusinessLogic.Services;
using Vintagefur.Domain.Models;
using Vintagefur.Web.Models;
using Vintagefur.Web.Models.ViewModels;

namespace Vintagefur.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ProductServiceBusinessLogic _productServiceBusinessLogic;

        public CartController()
        {
            _cartService = new CartServiceBusinessLogic();
            _productServiceBusinessLogic = new ProductServiceBusinessLogic();
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cart = _cartService.GetCart(HttpContext);
            var viewModel = new CartViewModel(cart);
            return View(viewModel);
        }

        // POST: Cart/AddToCart/5
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var product = _productServiceBusinessLogic.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            _cartService.AddItemToCart(HttpContext, id, quantity);

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
            _cartService.RemoveItemFromCart(HttpContext, id);
            return RedirectToAction("Index");
        }

        // POST: Cart/UpdateQuantity/5?quantity=3
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            _cartService.UpdateCartItemQuantity(HttpContext, id, quantity);
            return RedirectToAction("Index");
        }

        // POST: Cart/ClearCart
        [HttpPost]
        public ActionResult ClearCart()
        {
            _cartService.ClearCart(HttpContext);
            return RedirectToAction("Index");
        }

        // AJAX: Cart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = _cartService.GetCart(HttpContext);
            var viewModel = new CartViewModel(cart);
            ViewBag.CartCount = viewModel.TotalQuantity;
            return PartialView("_CartSummary");
        }
    }
} 