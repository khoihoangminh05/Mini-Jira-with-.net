using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalTaskManager.Core.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();
    }
}
