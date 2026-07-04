using System.Linq;
using System.Web.Mvc;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Repositories;
using PersonalTaskManager.Web.Helpers;
using PersonalTaskManager.Web.Models.Report;

namespace PersonalTaskManager.Web.Controllers
{
    [Authorize]
    public class ReportController : BaseController
    {
        private readonly IReportRepository _reportRepository;

        public ReportController()
        {
            _reportRepository = new ReportRepository();
        }

        /// <summary>Báo cáo % hoàn thành task theo từng project.</summary>
        public ActionResult Index()
        {
            var userId = RequireUserId();
            var summaries = _reportRepository.GetProjectProgressByUserId(userId);

            var items = summaries.Select(s => new ProjectProgressItemViewModel
            {
                ProjectId = s.ProjectId,
                ProjectName = s.ProjectName,
                TotalTasks = s.TotalTasks,
                DoneTasks = s.DoneTasks,
                ToDoTasks = s.ToDoTasks,
                InProgressTasks = s.InProgressTasks,
                PercentComplete = s.PercentComplete,
                ProgressBarClass = ReportHelper.GetProgressBarClass(s.PercentComplete),
                SummaryText = ReportHelper.FormatSummaryText(s.DoneTasks, s.TotalTasks, s.PercentComplete)
            }).ToList();

            var overallTotal = items.Sum(i => i.TotalTasks);
            var overallDone = items.Sum(i => i.DoneTasks);
            var overallPercent = overallTotal == 0 ? 0 : (int)System.Math.Round(overallDone * 100.0 / overallTotal);

            var model = new ReportIndexViewModel
            {
                Projects = items,
                OverallTotalTasks = overallTotal,
                OverallDoneTasks = overallDone,
                OverallPercentComplete = overallPercent
            };

            return View(model);
        }
    }
}
