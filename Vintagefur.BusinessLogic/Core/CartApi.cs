using System.Web;
using Vintagefur.Domain.Models;
using Vintagefur.Infrastructure.Data;

namespace Vintagefur.BusinessLogic.Core
{
    public class CartApi
    {
        private const string CartSessionKey = "CartItems";
        
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