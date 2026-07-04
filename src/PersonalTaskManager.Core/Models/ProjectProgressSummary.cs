namespace PersonalTaskManager.Core.Models
{
    /// <summary>Tổng hợp tiến độ task theo project — dùng cho báo cáo Phase 6.</summary>
    public class ProjectProgressSummary
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int TotalTasks { get; set; }

        public int DoneTasks { get; set; }

        public int ToDoTasks { get; set; }

        public int InProgressTasks { get; set; }

        public int PercentComplete { get; set; }
    }
}
