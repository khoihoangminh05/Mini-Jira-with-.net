using System.Collections.Generic;
using PersonalTaskManager.Core.Entities;

namespace PersonalTaskManager.Core.Interfaces
{
    public interface IActivityLogRepository
    {
        void Add(int userId, int? projectId, int? taskId, string actionType, string message);

        IList<ActivityLog> GetByProjectId(int projectId, int userId, int take = 30);

        IList<ActivityLog> GetRecentByUserId(int userId, int take = 10);
    }
}
