using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalTaskManager.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(256)]
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
