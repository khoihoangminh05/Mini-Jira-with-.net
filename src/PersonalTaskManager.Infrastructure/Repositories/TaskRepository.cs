using System;
using System.Collections.Generic;
using System.Linq;
using PersonalTaskManager.Core.Entities;
using PersonalTaskManager.Core.Enums;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Data;

namespace PersonalTaskManager.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public IList<WorkTask> GetActiveByProjectId(int projectId, int userId)
        {
            using (var context = new ApplicationDbContext())
            {
                // LINQ filter theo project + user sở hữu project
                return context.Tasks
                    .Where(t => t.ProjectId == projectId && !t.IsDeleted)
                    .Where(t => context.Projects.Any(
                        p => p.ProjectId == t.ProjectId && p.UserId == userId && !p.IsDeleted))
                    .OrderBy(t => t.Status)
                    .ThenBy(t => t.SortOrder)
                    .ThenByDescending(t => t.CreatedAt)
                    .ToList();
            }
        }

        public WorkTask GetByIdForUser(int taskId, int userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Tasks
                    .Where(t => t.TaskId == taskId && !t.IsDeleted)
                    .FirstOrDefault(t => context.Projects.Any(
                        p => p.ProjectId == t.ProjectId && p.UserId == userId && !p.IsDeleted));
            }
        }

        public WorkTask Create(
            int projectId,
            int userId,
            string title,
            string description,
            TaskPriority priority,
            TaskStatus status,
            DateTime? deadline)
        {
            using (var context = new ApplicationDbContext())
            {
                var project = context.Projects
                    .FirstOrDefault(p => p.ProjectId == projectId && p.UserId == userId && !p.IsDeleted);

                if (project == null)
                {
                    return null;
                }

                var maxSort = context.Tasks
                    .Where(t => t.ProjectId == projectId && !t.IsDeleted)
                    .Select(t => (int?)t.SortOrder)
                    .Max() ?? -1;

                var task = new WorkTask
                {
                    ProjectId = projectId,
                    Title = title.Trim(),
                    Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
                    Priority = priority,
                    Status = status,
                    Deadline = deadline,
                    SortOrder = maxSort + 1,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                };

                context.Tasks.Add(task);
                context.SaveChanges();
                return task;
            }
        }

        public bool Update(
            int taskId,
            int userId,
            string title,
            string description,
            TaskPriority priority,
            TaskStatus status,
            DateTime? deadline)
        {
            using (var context = new ApplicationDbContext())
            {
                var task = context.Tasks
                    .Where(t => t.TaskId == taskId && !t.IsDeleted)
                    .FirstOrDefault(t => context.Projects.Any(
                        p => p.ProjectId == t.ProjectId && p.UserId == userId && !p.IsDeleted));

                if (task == null)
                {
                    return false;
                }

                task.Title = title.Trim();
                task.Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
                task.Priority = priority;
                task.Status = status;
                task.Deadline = deadline;
                context.SaveChanges();
                return true;
            }
        }

        public bool SoftDelete(int taskId, int userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var task = context.Tasks
                    .Where(t => t.TaskId == taskId && !t.IsDeleted)
                    .FirstOrDefault(t => context.Projects.Any(
                        p => p.ProjectId == t.ProjectId && p.UserId == userId && !p.IsDeleted));

                if (task == null)
                {
                    return false;
                }

                task.IsDeleted = true;
                context.SaveChanges();
                return true;
            }
        }

        public bool UpdateStatus(int taskId, int userId, TaskStatus status)
        {
            using (var context = new ApplicationDbContext())
            {
                var task = context.Tasks
                    .Where(t => t.TaskId == taskId && !t.IsDeleted)
                    .FirstOrDefault(t => context.Projects.Any(
                        p => p.ProjectId == t.ProjectId && p.UserId == userId && !p.IsDeleted));

                if (task == null)
                {
                    return false;
                }

                task.Status = status;
                context.SaveChanges();
                return true;
            }
        }

        /// <summary>Drag-drop: đổi cột (status) + vị trí (sort order) trong transaction EF6.</summary>
        public bool MoveTask(int taskId, int userId, int projectId, TaskStatus newStatus, int newSortOrder)
        {
            using (var context = new ApplicationDbContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var projectExists = context.Projects.Any(
                        p => p.ProjectId == projectId && p.UserId == userId && !p.IsDeleted);
                    if (!projectExists)
                    {
                        return false;
                    }

                    var task = context.Tasks
                        .FirstOrDefault(t => t.TaskId == taskId && t.ProjectId == projectId && !t.IsDeleted);
                    if (task == null)
                    {
                        return false;
                    }

                    var oldStatus = task.Status;
                    var allTasks = context.Tasks
                        .Where(t => t.ProjectId == projectId && !t.IsDeleted)
                        .ToList();

                    var targetList = allTasks
                        .Where(t => t.Status == newStatus && t.TaskId != taskId)
                        .OrderBy(t => t.SortOrder)
                        .ThenBy(t => t.TaskId)
                        .ToList();

                    if (newSortOrder < 0)
                    {
                        newSortOrder = 0;
                    }

                    if (newSortOrder > targetList.Count)
                    {
                        newSortOrder = targetList.Count;
                    }

                    task.Status = newStatus;
                    targetList.Insert(newSortOrder, task);

                    for (var i = 0; i < targetList.Count; i++)
                    {
                        targetList[i].SortOrder = i;
                    }

                    if (oldStatus != newStatus)
                    {
                        var sourceList = allTasks
                            .Where(t => t.Status == oldStatus && t.TaskId != taskId)
                            .OrderBy(t => t.SortOrder)
                            .ThenBy(t => t.TaskId)
                            .ToList();

                        for (var i = 0; i < sourceList.Count; i++)
                        {
                            sourceList[i].SortOrder = i;
                        }
                    }

                    context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
