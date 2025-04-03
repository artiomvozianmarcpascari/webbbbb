using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vintagefur.Domain.Models;

namespace Vintagefur.Web.Models
{
    public class ShoppingCart
    {
        private const string CartSessionKey = "CartItems";
        
        // Получение корзины из сессии
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.LoadFromSession(context);
            return cart;
        }
        
        // Коллекция товаров в корзине
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        
        // Общая стоимость товаров в корзине
        public decimal TotalAmount => CartItems.Sum(i => i.TotalPrice);
        
        // Общее количество товаров в корзине
        public int TotalQuantity => CartItems.Sum(i => i.Quantity);
        
        // Добавление товара в корзину
        public void AddItem(Product product, int quantity = 1)
        {
            var existingItem = CartItems.FirstOrDefault(i => i.ProductId == product.Id);
            
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                CartItems.Add(new CartItem(product, quantity));
            }
        }
        
        // Удаление товара из корзины
        public void RemoveItem(int productId)
        {
            var item = CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                CartItems.Remove(item);
            }
        }
        
        // Обновление количества товара в корзине
        public void UpdateQuantity(int productId, int quantity)
        {
            var item = CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    RemoveItem(productId);
                }
            }
        }
        
        // Очистка корзины
        public void Clear()
        {
            CartItems.Clear();
        }
        
        // Сохранение корзины в сессию
        public void SaveToSession(HttpContextBase context)
        {
            context.Session[CartSessionKey] = CartItems;
        }
        
        // Загрузка корзины из сессии
        private void LoadFromSession(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] != null)
            {
                CartItems = (List<CartItem>)context.Session[CartSessionKey];
            }
        }
    }
} 