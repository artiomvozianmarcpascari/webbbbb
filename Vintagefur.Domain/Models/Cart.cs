using System;
using System.Collections.Generic;
using System.Linq;

namespace Vintagefur.Domain.Models
{
    [Serializable]
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        
        public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
        public int TotalQuantity => Items.Sum(i => i.Quantity);
        
        public Cart() { }
    }
} 