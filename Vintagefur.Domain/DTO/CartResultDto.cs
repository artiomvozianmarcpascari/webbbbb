using System;
using System.Collections.Generic;
using Vintagefur.Domain.Models;

namespace Vintagefur.Domain.DTO
{
    public class CartResultDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public Guid CartId { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
    }
} 