using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vintagefur.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }
        
        public bool IsAvailable { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        
        public int? MaterialId { get; set; }
        public virtual Material Material { get; set; }
        
        public int? StyleId { get; set; }
        public virtual ProductStyle Style { get; set; }
        
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
} 