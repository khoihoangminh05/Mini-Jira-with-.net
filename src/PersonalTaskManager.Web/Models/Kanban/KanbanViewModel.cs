using System;
using System.Collections.Generic;
using PersonalTaskManager.Core.Enums;

namespace PersonalTaskManager.Web.Models.Kanban
{
    public class KanbanViewModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public IList<KanbanTaskCardViewModel> ToDo { get; set; } = new List<KanbanTaskCardViewModel>();

        public IList<KanbanTaskCardViewModel> InProgress { get; set; } = new List<KanbanTaskCardViewModel>();

        public IList<KanbanTaskCardViewModel> Done { get; set; } = new List<KanbanTaskCardViewModel>();

        public IList<PersonalTaskManager.Web.Models.Activity.ActivityLogItemViewModel> ActivityLogs { get; set; }
            = new List<PersonalTaskManager.Web.Models.Activity.ActivityLogItemViewModel>();
    }

    public class KanbanTaskCardViewModel
    {
        public int TaskId { get; set; }

        public int ProjectId { get; set; }

        public string Title { get; set; }

        public TaskPriority Priority { get; set; }

        public string PriorityLabel { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime? Deadline { get; set; }

        public bool IsOverdue { get; set; }
    }
}
