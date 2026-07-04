using System;
using PersonalTaskManager.Core.Entities;
using PersonalTaskManager.Core.Enums;
using PersonalTaskManager.Web.Models.Kanban;

namespace PersonalTaskManager.Web.Helpers
{
    public static class TaskCardMapper
    {
        public static KanbanTaskCardViewModel FromEntity(WorkTask task)
        {
            var today = DateTime.Today;
            return new KanbanTaskCardViewModel
            {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Priority = task.Priority,
                PriorityLabel = TaskEnumHelper.GetPriorityLabel(task.Priority),
                Status = task.Status,
                Deadline = task.Deadline,
                IsOverdue = task.Deadline.HasValue
                    && task.Deadline.Value.Date < today
                    && task.Status != TaskStatus.Done
            };
        }

        public static string GetColumnKey(TaskStatus status)
        {
            switch (status)
            {
                case TaskStatus.ToDo: return "todo";
                case TaskStatus.InProgress: return "inprogress";
                case TaskStatus.Done: return "done";
                default: return "todo";
            }
        }
    }
}
