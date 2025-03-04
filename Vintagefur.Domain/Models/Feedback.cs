using System;
using System.ComponentModel.DataAnnotations;

namespace Vintagefur.Domain.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        [Range(1, 5)]
        public int? Rating { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        public bool IsPublished { get; set; }
    }
} 