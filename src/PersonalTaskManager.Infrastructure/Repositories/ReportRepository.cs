using System;
using System.Collections.Generic;
using System.Linq;
using PersonalTaskManager.Core.Enums;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Core.Models;
using PersonalTaskManager.Infrastructure.Data;

namespace PersonalTaskManager.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        public IList<ProjectProgressSummary> GetProjectProgressByUserId(int userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var projects = context.Projects
                    .Where(p => p.UserId == userId && !p.IsDeleted)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToList();

                var summaries = new List<ProjectProgressSummary>();

                foreach (var project in projects)
                {
                    var tasks = context.Tasks
                        .Where(t => t.ProjectId == project.ProjectId && !t.IsDeleted)
                        .ToList();

                    var total = tasks.Count;
                    var done = tasks.Count(t => t.Status == TaskStatus.Done);
                    var todo = tasks.Count(t => t.Status == TaskStatus.ToDo);
                    var inProgress = tasks.Count(t => t.Status == TaskStatus.InProgress);

                    // total = 0 → 0% (tránh chia cho 0)
                    var percent = total == 0 ? 0 : (int)Math.Round(done * 100.0 / total);

                    summaries.Add(new ProjectProgressSummary
                    {
                        ProjectId = project.ProjectId,
                        ProjectName = project.Name,
                        TotalTasks = total,
                        DoneTasks = done,
                        ToDoTasks = todo,
                        InProgressTasks = inProgress,
                        PercentComplete = percent
                    });
                }

                return summaries;
            }
        }
    }
}
