using System;

namespace Vintagefur.Domain.DTO
{
    public class CartActionDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int? UserId { get; set; }
        public Guid? CartId { get; set; }
        public string Action { get; set; } // Add, Remove, Update, Clear
    }
} 