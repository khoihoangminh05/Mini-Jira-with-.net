using System;
using System.Collections.Generic;
using PersonalTaskManager.Core.Enums;

namespace PersonalTaskManager.Web.Models.Task
{
    public class TaskListViewModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public IList<TaskListItemViewModel> Tasks { get; set; } = new List<TaskListItemViewModel>();
    }

    public class TaskListItemViewModel
    {
        public int TaskId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TaskPriority Priority { get; set; }

        public string PriorityLabel { get; set; }

        public TaskStatus Status { get; set; }

        public string StatusLabel { get; set; }

        public DateTime? Deadline { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
