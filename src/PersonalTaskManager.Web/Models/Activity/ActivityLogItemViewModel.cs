using System;
using System.Collections.Generic;

namespace PersonalTaskManager.Web.Models.Activity
{
    public class ActivityLogItemViewModel
    {
        public string ActionType { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public string TimeLabel => CreatedAt.ToString("dd/MM/yyyy HH:mm");
    }

    public class ActivityTimelineViewModel
    {
        public int ProjectId { get; set; }

        public IList<ActivityLogItemViewModel> Items { get; set; } = new List<ActivityLogItemViewModel>();
    }
}
