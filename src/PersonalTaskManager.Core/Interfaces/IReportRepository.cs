using System.Collections.Generic;
using PersonalTaskManager.Core.Models;

namespace PersonalTaskManager.Core.Interfaces
{
    public interface IReportRepository
    {
        IList<ProjectProgressSummary> GetProjectProgressByUserId(int userId);
    }
}
