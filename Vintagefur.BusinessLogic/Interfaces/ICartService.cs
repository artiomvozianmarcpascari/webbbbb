using System.Web;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Interfaces
{
    public interface ICartService
    {
        Cart GetCart(HttpContextBase context);
        void AddItemToCart(HttpContextBase context, int productId, int quantity = 1);
        void RemoveItemFromCart(HttpContextBase context, int productId);
        void UpdateCartItemQuantity(HttpContextBase context, int productId, int quantity);
        void ClearCart(HttpContextBase context);
    }
} 