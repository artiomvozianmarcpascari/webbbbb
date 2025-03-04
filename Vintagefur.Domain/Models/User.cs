using System;
using System.ComponentModel.DataAnnotations;

namespace Vintagefur.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool IsActive { get; set; }

        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
} 