using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vintagefur.Domain.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
} 