using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.BusinessLogic.Services;
using Vintagefur.Domain.Models;
using Vintagefur.Web.Models;
using Vintagefur.Web.Models.ViewModels;

namespace Vintagefur.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly CustomerServiceBusinessLogic _customerServiceBusinessLogic;
        private readonly OrderServiceBusinessLogic _orderServiceBusinessLogic;
        private readonly ProductServiceBusinessLogic _productServiceBusinessLogic;
        private readonly ICartService _cartService;

        public CheckoutController()
        {
            _customerServiceBusinessLogic = new CustomerServiceBusinessLogic();
            _orderServiceBusinessLogic = new OrderServiceBusinessLogic();
            _productServiceBusinessLogic = new ProductServiceBusinessLogic();
            _cartService = new CartServiceBusinessLogic();
        }

        // GET: Checkout
        public ActionResult Index()
        {
            var cart = _cartService.GetCart(HttpContext);
            var viewModel = new CartViewModel(cart);
            if (viewModel.CartItems.Count == 0)
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

            var cart = _cartService.GetCart(HttpContext);
            var viewModel = new CartViewModel(cart);
            if (viewModel.CartItems.Count == 0)
            {
                ModelState.AddModelError("", "Ваша корзина пуста");
                return View("Index", customer);
            }

            // Поиск существующего клиента по email или создание нового
            var existingCustomer = _customerServiceBusinessLogic.GetCustomerByEmail(customer.Email);
            int customerId;

            if (existingCustomer != null)
            {
                customerId = existingCustomer.Id;
                
                // Обновим данные клиента, если изменились
                customer.Id = customerId;
                _customerServiceBusinessLogic.UpdateCustomer(customer);
            }
            else
            {
                // Создаем нового клиента
                customerId = _customerServiceBusinessLogic.CreateCustomer(
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
            foreach (var item in viewModel.CartItems)
            {
                var product = _productServiceBusinessLogic.GetProductById(item.ProductId);
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
                int orderId = _orderServiceBusinessLogic.CreateOrder(
                    customer,
                    orderItems,
                    customer.Address,
                    customer.City,
                    customer.PostalCode,
                    customer.Country,
                    Request.Form["Notes"]
                );

                // Очищаем корзину после оформления заказа
                _cartService.ClearCart(HttpContext);

                // Перенаправление на страницу подтверждения заказа
                return RedirectToAction("OrderConfirmation", new { id = orderId });
            }

            ModelState.AddModelError("", "Произошла ошибка при оформлении заказа");
            return View("Index", customer);
        }

        // GET: Checkout/OrderConfirmation/5
        public ActionResult OrderConfirmation(int id)
        {
            var order = _orderServiceBusinessLogic.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
} 