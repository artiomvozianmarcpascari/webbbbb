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
            return base.GetCart(httpContext);
        }

        public void AddItemToCart(HttpContext httpContext, int productId, int quantity = 1)
        {
            // Валидация входных данных
            if (productId <= 0 || quantity <= 0)
                return;

            // Вызов метода базового класса
            base.AddItemToCart(httpContext, productId, quantity);
        }

        public void RemoveItemFromCart(HttpContext httpContext, int productId)
        {
            // Валидация входных данных
            if (productId <= 0)
                return;

            // Вызов метода базового класса
            base.RemoveItemFromCart(httpContext, productId);
        }

        public void UpdateCartItemQuantity(HttpContext httpContext, int productId, int quantity)
        {
            // Валидация входных данных
            if (productId <= 0 || quantity < 0)
                return;

            // Вызов метода базового класса
            base.UpdateCartItemQuantity(httpContext, productId, quantity);
        }

        public void ClearCart(HttpContext httpContext)
        {
            // Вызов метода базового класса
            base.ClearCart(httpContext);
        }
        
        public CartResultDto ProcessCartAction(CartActionDto actionDto)
        {
            // Вызов метода базового класса
            return base.ProcessCartAction(actionDto);
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
    }
} 