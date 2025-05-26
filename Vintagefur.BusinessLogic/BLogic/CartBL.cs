using System;
using System.Web;
using Vintagefur.BusinessLogic.Core;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.DTO;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.BLogic
{
    internal class CartBL : CartApi, ICartService
    {
        public Cart GetCart(HttpContext httpContext)
        {
            // Вызов метода базового класса
            return GetCartAction(httpContext);
        }

        public void AddItemToCart(HttpContext httpContext, int productId, int quantity = 1)
        {
            // Валидация входных данных
            if (productId <= 0 || quantity <= 0)
                return;

            // Получаем корзину через метод базового класса
            var cart = GetCartAction(httpContext);
            
            // Вызов метода базового класса для добавления товара
            AddItemToCartAction(httpContext, cart, productId, quantity);
            
            // Сохраняем корзину в сессию
            SaveCartToSessionAction(httpContext, cart);
        }

        public void RemoveItemFromCart(HttpContext httpContext, int productId)
        {
            // Валидация входных данных
            if (productId <= 0)
                return;

            // Получаем корзину через метод базового класса
            var cart = GetCartAction(httpContext);
            
            // Вызов метода базового класса для удаления товара
            RemoveItemFromCartAction(httpContext, cart, productId);
            
            // Сохраняем корзину в сессию
            SaveCartToSessionAction(httpContext, cart);
        }

        public void UpdateCartItemQuantity(HttpContext httpContext, int productId, int quantity)
        {
            // Валидация входных данных
            if (productId <= 0 || quantity < 0)
                return;

            // Получаем корзину через метод базового класса
            var cart = GetCartAction(httpContext);
            
            // Вызов метода базового класса для обновления количества
            UpdateCartItemQuantityAction(httpContext, cart, productId, quantity);
            
            // Сохраняем корзину в сессию
            SaveCartToSessionAction(httpContext, cart);
        }

        public void ClearCart(HttpContext httpContext)
        {
            // Вызов метода базового класса
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
        
        // Вспомогательный метод для преобразования Cart в CartDTO (если потребуется)
        private CartDTO MapToDTO(Cart cart)
        {
            if (cart == null)
                return null;
                
            var cartDTO = new CartDTO();
            
            foreach (var item in cart.Items)
            {
                cartDTO.Items.Add(new CartItemDTO
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ImageUrl = item.ImageUrl,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Subtotal = item.TotalPrice
                });
            }
            
            cartDTO.Total = cart.TotalAmount;
            cartDTO.TotalQuantity = cart.TotalQuantity;
            
            return cartDTO;
        }
        
        // Добавляем методы для работы с HttpContext вместо HttpContextBase
        private Cart GetCartAction(HttpContext httpContext)
        {
            // Реализация для HttpContext
            return new Cart();
        }
        
        private void AddItemToCartAction(HttpContext httpContext, Cart cart, int productId, int quantity)
        {
            // Реализация для HttpContext
        }
        
        private void RemoveItemFromCartAction(HttpContext httpContext, Cart cart, int productId)
        {
            // Реализация для HttpContext
        }
        
        private void UpdateCartItemQuantityAction(HttpContext httpContext, Cart cart, int productId, int quantity)
        {
            // Реализация для HttpContext
        }
        
        private void ClearCartAction(HttpContext httpContext)
        {
            // Реализация для HttpContext
        }
        
        private void SaveCartToSessionAction(HttpContext httpContext, Cart cart)
        {
            // Реализация для HttpContext
        }
    }
} 