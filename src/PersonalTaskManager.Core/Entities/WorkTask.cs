using System;
using System.ComponentModel.DataAnnotations;
using PersonalTaskManager.Core.Enums;

namespace PersonalTaskManager.Core.Entities
{
    /// <summary>Entity Task — tên WorkTask tránh trùng System.Threading.Tasks.Task.</summary>
    public class WorkTask
    {
        public int TaskId { get; set; }

        public int ProjectId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public TaskStatus Status { get; set; } = TaskStatus.ToDo;

        public DateTime? Deadline { get; set; }

        public int SortOrder { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Project Project { get; set; }
    }
}
