using System;

namespace Vintagefur.Domain.Models
{
    [Serializable]
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        
        public CartItem() { }
        
        public CartItem(int productId, string productName, string imageUrl, decimal unitPrice, int quantity = 1)
        {
            ProductId = productId;
            ProductName = productName;
            ImageUrl = imageUrl;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
} 