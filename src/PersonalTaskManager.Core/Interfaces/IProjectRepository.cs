using System.Collections.Generic;
using PersonalTaskManager.Core.Entities;

namespace PersonalTaskManager.Core.Interfaces
{
    public interface IProjectRepository
    {
        IList<Project> GetActiveByUserId(int userId);

        Project GetByIdForUser(int projectId, int userId);

        Project Create(int userId, string name, string description);

        bool Update(int projectId, int userId, string name, string description);

        bool SoftDelete(int projectId, int userId);
    }
}
