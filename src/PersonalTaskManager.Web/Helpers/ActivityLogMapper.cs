using System.Collections.Generic;
using System.Linq;
using PersonalTaskManager.Core.Entities;
using PersonalTaskManager.Web.Models.Activity;

namespace PersonalTaskManager.Web.Helpers
{
    public static class ActivityLogMapper
    {
        public static IList<ActivityLogItemViewModel> ToViewModels(IEnumerable<ActivityLog> logs)
        {
            return logs.Select(l => new ActivityLogItemViewModel
            {
                ActionType = l.ActionType,
                Message = l.Message,
                CreatedAt = l.CreatedAt
            }).ToList();
        }
    }
}
