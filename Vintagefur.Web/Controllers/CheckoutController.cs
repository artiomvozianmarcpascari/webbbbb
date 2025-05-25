using System;
using Vintagefur.BusinessLogic;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.Models;

namespace Vintagefur.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderBL _orderService;
        private readonly ICustomerBL _customerService;

        public CheckoutController()
        {
            var factory = BusinessLogicFactory.Instance;
            _cartService = factory.GetCartBL();
            _orderService = factory.GetOrderBL();
            _customerService = factory.GetCustomerBL();
        }

        // GET: Checkout
        public ActionResult Index()
        {
            var cart = _cartService.GetCart(System.Web.HttpContext.Current);
            if (cart.TotalQuantity <= 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            return View();
        }

        // POST: Checkout/PlaceOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var cart = _cartService.GetCart(System.Web.HttpContext.Current);
                if (cart.TotalQuantity <= 0)
                {
                    return RedirectToAction("Index", "Cart");
                }

                // Сохраняем информацию о клиенте
                _customerService.CreateCustomer(customer);

                // Создаем заказ
                var order = new Order
                {
                    CustomerId = customer.Id,
                    OrderDate = DateTime.Now,
                    TotalAmount = cart.TotalAmount,
                    Status = OrderStatus.Pending
                };

                if (_orderService.CreateOrder(order))
                {
                    // После успешного создания заказа очищаем корзину
                    _cartService.ClearCart(System.Web.HttpContext.Current);
                    return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
                }
            }

            return View("Index", customer);
        }

        // GET: Checkout/OrderConfirmation/5
        public ActionResult OrderConfirmation(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }
    }
} 