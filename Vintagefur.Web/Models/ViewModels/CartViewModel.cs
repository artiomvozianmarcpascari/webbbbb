using System.Collections.Generic;
using System.Linq;
using Vintagefur.Domain.Models;

namespace Vintagefur.Web.Models.ViewModels
{
    public class CartViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
        public decimal TotalAmount => CartItems.Sum(i => i.TotalPrice);
        public int TotalQuantity => CartItems.Sum(i => i.Quantity);

        public CartViewModel() { }

        public CartViewModel(Cart cart)
        {
            if (cart != null && cart.Items != null)
            {
                foreach (var item in cart.Items)
                {
                    CartItems.Add(new CartItemViewModel
                    {
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        ImageUrl = item.ImageUrl,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity
                    });
                }
            }
        }
    }

    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
} 