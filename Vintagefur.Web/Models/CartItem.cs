using Vintagefur.Domain.Models;

namespace Vintagefur.Web.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        
        public CartItem() { }
        
        public CartItem(Product product, int quantity = 1)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            ImageUrl = product.ImageUrl;
            UnitPrice = product.Price;
            Quantity = quantity;
        }
    }
} 