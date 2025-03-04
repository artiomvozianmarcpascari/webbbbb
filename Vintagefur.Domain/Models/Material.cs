using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vintagefur.Domain.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsNatural { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
} 