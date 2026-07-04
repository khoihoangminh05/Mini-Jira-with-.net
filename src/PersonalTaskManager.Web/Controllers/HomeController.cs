using System.Linq;
using System.Web.Mvc;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Data;
using PersonalTaskManager.Infrastructure.Repositories;
using PersonalTaskManager.Web.Helpers;
using PersonalTaskManager.Web.Models.Home;

namespace PersonalTaskManager.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IReportRepository _reportRepository;
        private readonly IActivityLogRepository _activityLogRepository;

        public HomeController()
        {
            _reportRepository = new ReportRepository();
            _activityLogRepository = new ActivityLogRepository();
        }

        /// <summary>Dashboard tổng quan — Phase 9.</summary>
        public ActionResult Index()
        {
            var userId = RequireUserId();
            var summary = _reportRepository.GetDashboardSummary(userId);

            var model = new DashboardViewModel
            {
                Username = CurrentUsername,
                ProjectCount = summary.ProjectCount,
                TotalTasks = summary.TotalTasks,
                DoneTasks = summary.DoneTasks,
                InProgressTasks = summary.InProgressTasks,
                ToDoTasks = summary.ToDoTasks,
                OverdueTasks = summary.OverdueTasks,
                Projects = summary.Projects.Select(p => new ProjectQuickLinkViewModel
                {
                    ProjectId = p.ProjectId,
                    ProjectName = p.ProjectName,
                    TotalTasks = p.TotalTasks,
                    DoneTasks = p.DoneTasks,
                    OverdueTasks = p.OverdueTasks
                }).ToList()
            };

            try
            {
                model.RecentActivity = ActivityLogMapper.ToViewModels(
                    _activityLogRepository.GetRecentByUserId(userId, 8));
            }
            catch
            {
                // ACTIVITY_LOG chưa migrate
            }

            return View(model);
        }

        /// <summary>Kiểm tra kết nối Oracle.</summary>
        public ActionResult About()
        {
            string message;
            ViewBag.DatabaseConnected = DatabaseConnectionTester.TryConnect(out message);
            ViewBag.DatabaseMessage = message;
            return View();
        }
    }
}
