using System;
using System.Collections.Generic;
using System.Web;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.DTO;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Services
{
    public class CartServiceBusinessLogic : ICartService
    {
        private readonly ICartService _cartService;
        
        public CartServiceBusinessLogic()
        {
            _cartService = BusinessLogicFactory.Instance.GetCartBL();
        }
        
        public Cart GetCart(HttpContext httpContext)
        {
            return _cartService.GetCart(httpContext);
        }
        
        public void AddItemToCart(HttpContext httpContext, int productId, int quantity)
        {
            _cartService.AddItemToCart(httpContext, productId, quantity);
        }
        
        public void RemoveItemFromCart(HttpContext httpContext, int productId)
        {
            _cartService.RemoveItemFromCart(httpContext, productId);
        }
        
        public void UpdateCartItemQuantity(HttpContext httpContext, int productId, int quantity)
        {
            _cartService.UpdateCartItemQuantity(httpContext, productId, quantity);
        }
        
        public void ClearCart(HttpContext httpContext)
        {
            _cartService.ClearCart(httpContext);
        }
        
        public CartResultDto ProcessCartAction(CartActionDto actionDto)
        {
            return _cartService.ProcessCartAction(actionDto);
        }
    }
} 