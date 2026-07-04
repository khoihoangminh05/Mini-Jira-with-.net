using System.Web.Mvc;
using PersonalTaskManager.Core.Enums;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Repositories;

namespace PersonalTaskManager.Web.Helpers
{
    /// <summary>Logic chuyển trạng thái task — dùng chung Kanban POST và Ajax JSON.</summary>
    public static class TaskStatusTransition
    {
        public static bool TryApply(
            ITaskRepository repository,
            int taskId,
            int userId,
            int projectId,
            string action,
            out TaskStatus newStatus,
            out string message)
        {
            newStatus = TaskStatus.ToDo;
            message = null;

            var task = repository.GetByIdForUser(taskId, userId);
            if (task == null || task.ProjectId != projectId)
            {
                message = "Task không tồn tại hoặc bạn không có quyền.";
                return false;
            }

            switch (action?.ToLowerInvariant())
            {
                case "start":
                    if (task.Status != TaskStatus.ToDo)
                    {
                        message = "Task không ở trạng thái Cần làm.";
                        return false;
                    }

                    newStatus = TaskStatus.InProgress;
                    message = "Đã chuyển task sang Đang làm.";
                    break;

                case "complete":
                    if (task.Status != TaskStatus.InProgress)
                    {
                        message = "Task không ở trạng thái Đang làm.";
                        return false;
                    }

                    newStatus = TaskStatus.Done;
                    message = "Đã hoàn thành task.";
                    break;

                case "reopen":
                    if (task.Status != TaskStatus.Done && task.Status != TaskStatus.InProgress)
                    {
                        message = "Không thể mở lại task này.";
                        return false;
                    }

                    newStatus = TaskStatus.ToDo;
                    message = "Đã mở lại task.";
                    break;

                default:
                    message = "Hành động không hợp lệ.";
                    return false;
            }

            if (!repository.UpdateStatus(taskId, userId, newStatus))
            {
                message = "Không thể cập nhật trạng thái.";
                return false;
            }

            return true;
        }
    }
}
