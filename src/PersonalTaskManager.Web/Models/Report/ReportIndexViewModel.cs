using System.Collections.Generic;

namespace PersonalTaskManager.Web.Models.Report
{
    public class ReportIndexViewModel
    {
        public IList<ProjectProgressItemViewModel> Projects { get; set; } = new List<ProjectProgressItemViewModel>();

        public int OverallTotalTasks { get; set; }

        public int OverallDoneTasks { get; set; }

        public int OverallPercentComplete { get; set; }
    }

    public class ProjectProgressItemViewModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int TotalTasks { get; set; }

        public int DoneTasks { get; set; }

        public int ToDoTasks { get; set; }

        public int InProgressTasks { get; set; }

        public int PercentComplete { get; set; }

        public string ProgressBarClass { get; set; }

        public string SummaryText { get; set; }
    }
}
