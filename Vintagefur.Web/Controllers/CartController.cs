using System;
using System.Web.Mvc;
using Vintagefur.BusinessLogic;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.DTO;
using Vintagefur.Web.Models.ViewModels;

namespace Vintagefur.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProduct _productService;

        public CartController()
        {
            var factory = BusinessLogicFactory.Instance;
            _cartService = factory.GetCartBL();
            _productService = factory.GetProductBL();
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cart = _cartService.GetCart(System.Web.HttpContext.Current);
            var viewModel = new CartViewModel(cart);
            return View(viewModel);
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

            var cartAction = new CartActionDto
            {
                ProductId = id,
                Quantity = quantity,
                Action = "Add"
            };
            
            // Получаем текущего пользователя, если он авторизован
            if (User.Identity.IsAuthenticated)
            {
                // Предполагаем, что имя пользователя - это его email
                var email = User.Identity.Name;
                var userBL = BusinessLogicFactory.Instance.GetUserBL();
                var user = userBL.GetUserByEmail(email);
                if (user != null)
                {
                    cartAction.UserId = user.Id;
                }
            }
            
            // Получаем текущий CartId из сессии или куки
            if (Session["CartId"] != null)
            {
                cartAction.CartId = (Guid)Session["CartId"];
            }
            
            var result = _cartService.ProcessCartAction(cartAction);
            if (result.IsSuccess)
            {
                // Сохраняем CartId в сессии, если его не было
                if (Session["CartId"] == null)
                {
                    Session["CartId"] = result.CartId;
                }
            }

            // После добавления в корзину, перенаправляем обратно на страницу товара
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
            var cartAction = new CartActionDto
            {
                ProductId = id,
                Action = "Remove"
            };
            
            if (Session["CartId"] != null)
            {
                cartAction.CartId = (Guid)Session["CartId"];
            }
            
            _cartService.ProcessCartAction(cartAction);
            return RedirectToAction("Index");
        }

        // POST: Cart/UpdateQuantity/5?quantity=3
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var cartAction = new CartActionDto
            {
                ProductId = id,
                Quantity = quantity,
                Action = "Update"
            };
            
            if (Session["CartId"] != null)
            {
                cartAction.CartId = (Guid)Session["CartId"];
            }
            
            _cartService.ProcessCartAction(cartAction);
            return RedirectToAction("Index");
        }

        // POST: Cart/ClearCart
        [HttpPost]
        public ActionResult ClearCart()
        {
            var cartAction = new CartActionDto
            {
                Action = "Clear"
            };
            
            if (Session["CartId"] != null)
            {
                cartAction.CartId = (Guid)Session["CartId"];
            }
            
            _cartService.ProcessCartAction(cartAction);
            return RedirectToAction("Index");
        }

        // AJAX: Cart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cartId = Session["CartId"] as Guid?;
            var cartAction = new CartActionDto
            {
                Action = "Get",
                CartId = cartId
            };
            
            var cart = _cartService.ProcessCartAction(cartAction);
            ViewBag.CartCount = cart.TotalItems;
            return PartialView("_CartSummary");
        }
    }
} 