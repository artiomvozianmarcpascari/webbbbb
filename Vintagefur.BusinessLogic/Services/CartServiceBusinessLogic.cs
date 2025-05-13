using System.Web;
using System.Linq;
using Vintagefur.BusinessLogic.Interfaces;
using Vintagefur.Domain.Models;

namespace Vintagefur.BusinessLogic.Services
{
    public class CartServiceBusinessLogic : ICartService
    {
        private readonly ProductServiceBusinessLogic _productServiceBusinessLogic;
        private const string CartSessionKey = "CartItems";

        public CartServiceBusinessLogic()
        {
            _productServiceBusinessLogic = new ProductServiceBusinessLogic();
        }

        public Cart GetCart(HttpContextBase context)
        {
            var cart = new Cart();
            
            if (context.Session[CartSessionKey] != null)
            {
                try
                {
                    // Попробуем прочитать уже существующую корзину из сессии
                    var sessionCart = context.Session[CartSessionKey];
                    
                    // Если это список CartItem из нашего Web-проекта
                    if (sessionCart is System.Collections.Generic.List<object>)
                    {
                        var webCartItems = (System.Collections.Generic.List<object>)sessionCart;
                        
                        foreach (var item in webCartItems)
                        {
                            // Используем рефлексию для извлечения данных
                            var type = item.GetType();
                            
                            int productId = (int)type.GetProperty("ProductId").GetValue(item);
                            string productName = (string)type.GetProperty("ProductName").GetValue(item);
                            string imageUrl = (string)type.GetProperty("ImageUrl").GetValue(item);
                            decimal unitPrice = (decimal)type.GetProperty("UnitPrice").GetValue(item);
                            int quantity = (int)type.GetProperty("Quantity").GetValue(item);
                            
                            cart.Items.Add(new CartItem(productId, productName, imageUrl, unitPrice, quantity));
                        }
                    }
                }
                catch
                {
                    // Если произошла ошибка при чтении корзины, вернем пустую
                    cart = new Cart();
                }
            }
            
            return cart;
        }

        public void AddItemToCart(HttpContextBase context, int productId, int quantity = 1)
        {
            var product = _productServiceBusinessLogic.GetProductById(productId);
            if (product != null)
            {
                var cart = GetCart(context);
                var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
                
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    cart.Items.Add(new CartItem(
                        productId, 
                        product.Name, 
                        product.ImageUrl, 
                        product.Price, 
                        quantity));
                }
                
                SaveCartToSession(context, cart);
            }
        }

        public void RemoveItemFromCart(HttpContextBase context, int productId)
        {
            var cart = GetCart(context);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            
            if (item != null)
            {
                cart.Items.Remove(item);
                SaveCartToSession(context, cart);
            }
        }

        public void UpdateCartItemQuantity(HttpContextBase context, int productId, int quantity)
        {
            var cart = GetCart(context);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            
            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    cart.Items.Remove(item);
                }
                
                SaveCartToSession(context, cart);
            }
        }

        public void ClearCart(HttpContextBase context)
        {
            var cart = new Cart();
            SaveCartToSession(context, cart);
        }
        
        private void SaveCartToSession(HttpContextBase context, Cart cart)
        {
            // Сохраняем корзину непосредственно в сессию
            context.Session[CartSessionKey] = cart.Items;
        }
    }
} 