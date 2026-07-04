using System.Collections.Generic;
using PersonalTaskManager.Web.Models.Activity;

namespace PersonalTaskManager.Web.Models.Home
{
    public class DashboardViewModel
    {
        public string Username { get; set; }

        public int ProjectCount { get; set; }

        public int TotalTasks { get; set; }

        public int DoneTasks { get; set; }

        public int InProgressTasks { get; set; }

        public int ToDoTasks { get; set; }

        public int OverdueTasks { get; set; }

        public IList<ProjectQuickLinkViewModel> Projects { get; set; } = new List<ProjectQuickLinkViewModel>();

        public IList<ActivityLogItemViewModel> RecentActivity { get; set; } = new List<ActivityLogItemViewModel>();
    }

    public class ProjectQuickLinkViewModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int TotalTasks { get; set; }

        public int DoneTasks { get; set; }

        public int OverdueTasks { get; set; }
    }
}
