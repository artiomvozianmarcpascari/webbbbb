using System;
using System.Web;
using Vintagefur.Domain.DTO;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Data;

namespace Vintagefur.BusinessLogic.Core
{
    public class CartApi
    {
        private const string CartSessionKey = "CartItems";
        
        // Публичные методы для вызова из бизнес-логики
        public Cart GetCart(HttpContext httpContext)
        {
            return GetCartAction(httpContext);
        }
        
        public void AddItemToCart(HttpContext httpContext, int productId, int quantity)
        {
            var cart = GetCartAction(httpContext);
            AddItemToCartAction(httpContext, cart, productId, quantity);
            SaveCartToSessionAction(httpContext, cart);
        }
        
        public void RemoveItemFromCart(HttpContext httpContext, int productId)
        {
            var cart = GetCartAction(httpContext);
            RemoveItemFromCartAction(httpContext, cart, productId);
            SaveCartToSessionAction(httpContext, cart);
        }
        
        public void UpdateCartItemQuantity(HttpContext httpContext, int productId, int quantity)
        {
            var cart = GetCartAction(httpContext);
            UpdateCartItemQuantityAction(httpContext, cart, productId, quantity);
            SaveCartToSessionAction(httpContext, cart);
        }
        
        public void ClearCart(HttpContext httpContext)
        {
            ClearCartAction(httpContext);
        }
        
        public CartResultDto ProcessCartAction(CartActionDto actionDto)
        {
            try
            {
                if (actionDto == null)
                    return new CartResultDto { IsSuccess = false, ErrorMessage = "Недопустимые параметры запроса" };
                
                // Создаем результат по умолчанию
                var result = new CartResultDto
                {
                    IsSuccess = true,
                    CartId = actionDto.CartId ?? Guid.NewGuid(),
                    Items = new System.Collections.Generic.List<CartItem>(),
                    TotalPrice = 0,
                    TotalItems = 0
                };
                
                // Используем HttpContext.Current если он доступен
                var httpContext = HttpContext.Current;
                if (httpContext == null)
                    return new CartResultDto { IsSuccess = false, ErrorMessage = "HttpContext недоступен" };
                
                // Получаем корзину
                var cart = GetCartAction(httpContext);
                
                // Реализуем логику в зависимости от типа действия
                switch (actionDto.Action?.ToLower())
                {
                    case "add":
                        if (actionDto.ProductId <= 0 || actionDto.Quantity <= 0)
                            return new CartResultDto { IsSuccess = false, ErrorMessage = "Недопустимые параметры товара" };
                            
                        AddItemToCartAction(httpContext, cart, actionDto.ProductId, actionDto.Quantity);
                        break;
                        
                    case "remove":
                        if (actionDto.ProductId <= 0)
                            return new CartResultDto { IsSuccess = false, ErrorMessage = "Недопустимый ID товара" };
                            
                        RemoveItemFromCartAction(httpContext, cart, actionDto.ProductId);
                        break;
                        
                    case "update":
                        if (actionDto.ProductId <= 0 || actionDto.Quantity < 0)
                            return new CartResultDto { IsSuccess = false, ErrorMessage = "Недопустимые параметры для обновления" };
                            
                        UpdateCartItemQuantityAction(httpContext, cart, actionDto.ProductId, actionDto.Quantity);
                        break;
                        
                    case "clear":
                        ClearCartAction(httpContext);
                        break;
                        
                    case "get":
                        // Просто получаем текущую корзину
                        break;
                        
                    default:
                        return new CartResultDto { IsSuccess = false, ErrorMessage = "Неизвестное действие" };
                }
                
                // Сохраняем корзину в сессию
                SaveCartToSessionAction(httpContext, cart);
                
                // Преобразуем корзину в результат
                foreach (var item in cart.Items)
                {
                    result.Items.Add(item);
                    result.TotalPrice += item.TotalPrice;
                    result.TotalItems += item.Quantity;
                }
                
                return result;
            }
            catch (Exception ex)
            {
                return new CartResultDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
        
        // Защищенные методы для внутренней реализации
        protected Cart GetCartAction(HttpContextBase context)
        {
            // Здесь должен быть код для получения корзины из контекста
            return new Cart();
        }
        
        // Добавляем метод для работы с HttpContext вместо HttpContextBase
        protected Cart GetCartAction(HttpContext httpContext)
        {
            // Преобразуем HttpContext в HttpContextBase
            HttpContextBase contextBase = new HttpContextWrapper(httpContext);
            return GetCartAction(contextBase);
        }

        protected Product GetProductByIdAction(int productId)
        {
            using (var db = new VintagefurDbContext())
            {
                return db.Products.Find(productId);
            }
        }

        protected void AddItemToCartAction(HttpContextBase context, Cart cart, int productId, int quantity)
        {
            // Здесь должен быть код для добавления товара в корзину
        }
        
        // Добавляем метод для работы с HttpContext вместо HttpContextBase
        protected void AddItemToCartAction(HttpContext httpContext, Cart cart, int productId, int quantity)
        {
            // Преобразуем HttpContext в HttpContextBase
            HttpContextBase contextBase = new HttpContextWrapper(httpContext);
            AddItemToCartAction(contextBase, cart, productId, quantity);
        }

        protected void RemoveItemFromCartAction(HttpContextBase context, Cart cart, int productId)
        {
            // Здесь должен быть код для удаления товара из корзины
        }
        
        // Добавляем метод для работы с HttpContext вместо HttpContextBase
        protected void RemoveItemFromCartAction(HttpContext httpContext, Cart cart, int productId)
        {
            // Преобразуем HttpContext в HttpContextBase
            HttpContextBase contextBase = new HttpContextWrapper(httpContext);
            RemoveItemFromCartAction(contextBase, cart, productId);
        }

        protected void UpdateCartItemQuantityAction(HttpContextBase context, Cart cart, int productId, int quantity)
        {
            // Здесь должен быть код для обновления количества товара
        }
        
        // Добавляем метод для работы с HttpContext вместо HttpContextBase
        protected void UpdateCartItemQuantityAction(HttpContext httpContext, Cart cart, int productId, int quantity)
        {
            // Преобразуем HttpContext в HttpContextBase
            HttpContextBase contextBase = new HttpContextWrapper(httpContext);
            UpdateCartItemQuantityAction(contextBase, cart, productId, quantity);
        }

        protected void ClearCartAction(HttpContextBase context)
        {
            // Здесь должен быть код для очистки корзины
        }
        
        // Добавляем метод для работы с HttpContext вместо HttpContextBase
        protected void ClearCartAction(HttpContext httpContext)
        {
            // Преобразуем HttpContext в HttpContextBase
            HttpContextBase contextBase = new HttpContextWrapper(httpContext);
            ClearCartAction(contextBase);
        }
        
        protected void SaveCartToSessionAction(HttpContextBase context, Cart cart)
        {
            context.Session[CartSessionKey] = cart.Items;
        }
        
        // Добавляем метод для работы с HttpContext вместо HttpContextBase
        protected void SaveCartToSessionAction(HttpContext httpContext, Cart cart)
        {
            // Преобразуем HttpContext в HttpContextBase
            HttpContextBase contextBase = new HttpContextWrapper(httpContext);
            SaveCartToSessionAction(contextBase, cart);
        }
    }
} 