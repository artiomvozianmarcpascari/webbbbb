using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vintagefur.Domain.Models
{
    public class ProductStyle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Period { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
} 