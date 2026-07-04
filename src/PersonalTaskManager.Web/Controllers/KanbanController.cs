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

        public KanbanController()
        {
            _taskRepository = new TaskRepository();
            _projectRepository = new ProjectRepository();
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

            return View(model);
        }

        /// <summary>Fallback khi tắt JavaScript — POST form thông thường.</summary>
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
            TaskStatus newStatus;
            string message;

            if (!TaskStatusTransition.TryApply(_taskRepository, id, userId, projectId, action, out newStatus, out message))
            {
                return HttpNotFound();
            }

            TempData["SuccessMessage"] = message;
            return RedirectToAction("Index", new { projectId });
        }
    }
}
