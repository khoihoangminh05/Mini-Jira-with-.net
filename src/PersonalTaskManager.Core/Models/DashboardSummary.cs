using System.Collections.Generic;

namespace PersonalTaskManager.Core.Models
{
    public class DashboardSummary
    {
        public int ProjectCount { get; set; }

        public int TotalTasks { get; set; }

        public int DoneTasks { get; set; }

        public int InProgressTasks { get; set; }

        public int ToDoTasks { get; set; }

        public int OverdueTasks { get; set; }

        public IList<ProjectQuickLink> Projects { get; set; } = new List<ProjectQuickLink>();
    }

    public class ProjectQuickLink
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int TotalTasks { get; set; }

        public int DoneTasks { get; set; }

        public int OverdueTasks { get; set; }
    }
}
