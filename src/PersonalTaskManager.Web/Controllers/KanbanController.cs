using System.Linq;
using System.Web.Mvc;
using PersonalTaskManager.Core.Enums;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Repositories;
using PersonalTaskManager.Web.Helpers;
using PersonalTaskManager.Web.Models.Kanban;

namespace PersonalTaskManager.Web.Controllers
{
    [Authorize]
    public class KanbanController : BaseController
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IActivityLogRepository _activityLogRepository;

        public KanbanController()
        {
            _taskRepository = new TaskRepository();
            _projectRepository = new ProjectRepository();
            _activityLogRepository = new ActivityLogRepository();
        }

        public ActionResult Index(int projectId)
        {
            var userId = RequireUserId();
            var project = _projectRepository.GetByIdForUser(projectId, userId);
            if (project == null)
            {
                return HttpNotFound();
            }

            var tasks = _taskRepository.GetActiveByProjectId(projectId, userId);
            var cards = tasks.Select(TaskCardMapper.FromEntity).ToList();

            var model = new KanbanViewModel
            {
                ProjectId = project.ProjectId,
                ProjectName = project.Name,
                ToDo = cards.Where(c => c.Status == TaskStatus.ToDo).ToList(),
                InProgress = cards.Where(c => c.Status == TaskStatus.InProgress).ToList(),
                Done = cards.Where(c => c.Status == TaskStatus.Done).ToList()
            };

            try
            {
                model.ActivityLogs = ActivityLogMapper.ToViewModels(
                    _activityLogRepository.GetByProjectId(projectId, userId, 20));
            }
            catch
            {
                model.ActivityLogs = new System.Collections.Generic.List<Models.Activity.ActivityLogItemViewModel>();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Start(int id, int projectId)
        {
            return ChangeStatus(id, projectId, "start");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Complete(int id, int projectId)
        {
            return ChangeStatus(id, projectId, "complete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reopen(int id, int projectId)
        {
            return ChangeStatus(id, projectId, "reopen");
        }

        private ActionResult ChangeStatus(int id, int projectId, string action)
        {
            var userId = RequireUserId();
            var task = _taskRepository.GetByIdForUser(id, userId);
            TaskStatus newStatus;
            string message;

            if (!TaskStatusTransition.TryApply(_taskRepository, id, userId, projectId, action, out newStatus, out message))
            {
                return HttpNotFound();
            }

            if (task != null)
            {
                ActivityLogger.TryLog(
                    userId,
                    projectId,
                    id,
                    "StatusChange",
                    string.Format("Task \"{0}\": {1}", task.Title, message));
            }

            TempData["SuccessMessage"] = message;
            return RedirectToAction("Index", new { projectId });
        }
    }
}
