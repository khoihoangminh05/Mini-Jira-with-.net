using System;
using System.Collections.Generic;
using PersonalTaskManager.Core.Entities;
using PersonalTaskManager.Core.Enums;

namespace PersonalTaskManager.Core.Interfaces
{
    public interface ITaskRepository
    {
        IList<WorkTask> GetActiveByProjectId(int projectId, int userId);

        WorkTask GetByIdForUser(int taskId, int userId);

        WorkTask Create(
            int projectId,
            int userId,
            string title,
            string description,
            TaskPriority priority,
            TaskStatus status,
            DateTime? deadline);

        bool Update(
            int taskId,
            int userId,
            string title,
            string description,
            TaskPriority priority,
            TaskStatus status,
            DateTime? deadline);

        bool SoftDelete(int taskId, int userId);

        bool UpdateStatus(int taskId, int userId, TaskStatus status);

        bool MoveTask(int taskId, int userId, int projectId, TaskStatus newStatus, int newSortOrder);
    }
}
