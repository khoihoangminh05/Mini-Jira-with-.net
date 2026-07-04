using System;
using System.Collections.Generic;
using System.Linq;
using PersonalTaskManager.Core.Entities;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Data;

namespace PersonalTaskManager.Infrastructure.Repositories
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        public void Add(int userId, int? projectId, int? taskId, string actionType, string message)
        {
            using (var context = new ApplicationDbContext())
            {
                context.ActivityLogs.Add(new ActivityLog
                {
                    UserId = userId,
                    ProjectId = projectId,
                    TaskId = taskId,
                    ActionType = actionType,
                    Message = message,
                    CreatedAt = DateTime.Now
                });
                context.SaveChanges();
            }
        }

        public IList<ActivityLog> GetByProjectId(int projectId, int userId, int take = 30)
        {
            using (var context = new ApplicationDbContext())
            {
                var ownsProject = context.Projects.Any(
                    p => p.ProjectId == projectId && p.UserId == userId && !p.IsDeleted);
                if (!ownsProject)
                {
                    return new List<ActivityLog>();
                }

                return context.ActivityLogs
                    .Where(a => a.ProjectId == projectId && a.UserId == userId)
                    .OrderByDescending(a => a.CreatedAt)
                    .Take(take)
                    .ToList();
            }
        }

        public IList<ActivityLog> GetRecentByUserId(int userId, int take = 10)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.ActivityLogs
                    .Where(a => a.UserId == userId)
                    .OrderByDescending(a => a.CreatedAt)
                    .Take(take)
                    .ToList();
            }
        }
    }
}
