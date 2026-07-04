using System.Collections.Generic;
using System.Web.Mvc;
using PersonalTaskManager.Core.Enums;

namespace PersonalTaskManager.Web.Helpers
{
    public static class TaskEnumHelper
    {
        public static string GetPriorityLabel(TaskPriority priority)
        {
            switch (priority)
            {
                case TaskPriority.High: return "Cao";
                case TaskPriority.Medium: return "Trung bình";
                case TaskPriority.Low: return "Thấp";
                default: return priority.ToString();
            }
        }

        public static string GetStatusLabel(TaskStatus status)
        {
            switch (status)
            {
                case TaskStatus.ToDo: return "Cần làm";
                case TaskStatus.InProgress: return "Đang làm";
                case TaskStatus.Done: return "Hoàn thành";
                default: return status.ToString();
            }
        }

        public static string GetPriorityBadgeClass(TaskPriority priority)
        {
            switch (priority)
            {
                case TaskPriority.High: return "bg-danger";
                case TaskPriority.Medium: return "bg-warning text-dark";
                case TaskPriority.Low: return "bg-secondary";
                default: return "bg-light text-dark";
            }
        }

        public static string GetStatusBadgeClass(TaskStatus status)
        {
            switch (status)
            {
                case TaskStatus.ToDo: return "bg-secondary";
                case TaskStatus.InProgress: return "bg-primary";
                case TaskStatus.Done: return "bg-success";
                default: return "bg-light text-dark";
            }
        }

        public static SelectList GetPrioritySelectList(TaskPriority? selected = null)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem { Value = ((int)TaskPriority.High).ToString(), Text = GetPriorityLabel(TaskPriority.High) },
                new SelectListItem { Value = ((int)TaskPriority.Medium).ToString(), Text = GetPriorityLabel(TaskPriority.Medium) },
                new SelectListItem { Value = ((int)TaskPriority.Low).ToString(), Text = GetPriorityLabel(TaskPriority.Low) }
            };

            return new SelectList(items, "Value", "Text", selected.HasValue ? ((int)selected.Value).ToString() : null);
        }

        public static SelectList GetStatusSelectList(TaskStatus? selected = null)
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem { Value = ((int)TaskStatus.ToDo).ToString(), Text = GetStatusLabel(TaskStatus.ToDo) },
                new SelectListItem { Value = ((int)TaskStatus.InProgress).ToString(), Text = GetStatusLabel(TaskStatus.InProgress) },
                new SelectListItem { Value = ((int)TaskStatus.Done).ToString(), Text = GetStatusLabel(TaskStatus.Done) }
            };

            return new SelectList(items, "Value", "Text", selected.HasValue ? ((int)selected.Value).ToString() : null);
        }
    }
}
