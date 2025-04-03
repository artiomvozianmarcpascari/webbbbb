using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vintagefur.Application.Services;
using Vintagefur.Domain.Models;
using Vintagefur.Web.Models;

namespace Vintagefur.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly CustomerService _customerService;
        private readonly OrderService _orderService;
        private readonly ProductService _productService;

        public CheckoutController()
        {
            _customerService = new CustomerService();
            _orderService = new OrderService();
            _productService = new ProductService();
        }

        // GET: Checkout
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            if (cart.CartItems.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            var customer = new Customer();
            return View(customer);
        }

        // POST: Checkout/PlaceOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", customer);
            }

            var cart = ShoppingCart.GetCart(HttpContext);
            if (cart.CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Ваша корзина пуста");
                return View("Index", customer);
            }

            // Поиск существующего клиента по email или создание нового
            var existingCustomer = _customerService.GetCustomerByEmail(customer.Email);
            int customerId;

            if (existingCustomer != null)
            {
                customerId = existingCustomer.Id;
                
                // Обновим данные клиента, если изменились
                customer.Id = customerId;
                _customerService.UpdateCustomer(customer);
            }
            else
            {
                // Создаем нового клиента
                customerId = _customerService.CreateCustomer(
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.Address,
                    customer.City,
                    customer.PostalCode,
                    customer.Country
                );
                customer.Id = customerId;
            }

            // Создаем элементы заказа из корзины
            var orderItems = new List<OrderItem>();
            foreach (var item in cart.CartItems)
            {
                var product = _productService.GetProductById(item.ProductId);
                if (product != null)
                {
                    orderItems.Add(new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    });
                }
            }

            if (orderItems.Count > 0)
            {
                // Создаем заказ
                int orderId = _orderService.CreateOrder(
                    customer,
                    orderItems,
                    customer.Address,
                    customer.City,
                    customer.PostalCode,
                    customer.Country,
                    Request.Form["Notes"]
                );

                // Очищаем корзину после оформления заказа
                cart.Clear();
                cart.SaveToSession(HttpContext);

                // Перенаправление на страницу подтверждения заказа
                return RedirectToAction("OrderConfirmation", new { id = orderId });
            }

            ModelState.AddModelError("", "Произошла ошибка при оформлении заказа");
            return View("Index", customer);
        }

        // GET: Checkout/OrderConfirmation/5
        public ActionResult OrderConfirmation(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
} 