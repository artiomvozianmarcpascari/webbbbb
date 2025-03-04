using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vintagefur.Domain.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? ShippingDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [StringLength(500)]
        public string ShippingAddress { get; set; }

        [StringLength(100)]
        public string ShippingCity { get; set; }

        [StringLength(20)]
        public string ShippingPostalCode { get; set; }

        [StringLength(100)]
        public string ShippingCountry { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
} 