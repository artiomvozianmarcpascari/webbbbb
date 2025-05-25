using System;
using System.Web;
using Vintagefur.Domain.DTO;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Interfaces
{
    public interface ICartService
    {
        Cart GetCart(HttpContext httpContext);
        void AddItemToCart(HttpContext httpContext, int productId, int quantity);
        void RemoveItemFromCart(HttpContext httpContext, int productId);
        void UpdateCartItemQuantity(HttpContext httpContext, int productId, int quantity);
        void ClearCart(HttpContext httpContext);
        
        // Новый метод для работы с DTO
        CartResultDto ProcessCartAction(CartActionDto actionDto);
    }
} 