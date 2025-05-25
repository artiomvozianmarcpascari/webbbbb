using System.Collections.Generic;

namespace Vintagefur.Domain.DTO
{
    public class CartDTO
    {
        public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
        public decimal Total { get; set; }
        public int TotalQuantity { get; set; }
    }
    
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
} 