using System;
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
            var cards = tasks.Select(MapToCard).ToList();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Start(int id, int projectId)
        {
            return ChangeStatus(id, projectId, TaskStatus.ToDo, TaskStatus.InProgress, "Đã chuyển task sang Đang làm.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Complete(int id, int projectId)
        {
            return ChangeStatus(id, projectId, TaskStatus.InProgress, TaskStatus.Done, "Đã hoàn thành task.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reopen(int id, int projectId)
        {
            var userId = RequireUserId();
            var task = _taskRepository.GetByIdForUser(id, userId);
            if (task == null || task.ProjectId != projectId)
            {
                return HttpNotFound();
            }

            // Reopen từ Done hoặc InProgress về To Do
            if (task.Status != TaskStatus.Done && task.Status != TaskStatus.InProgress)
            {
                return HttpNotFound();
            }

            if (!_taskRepository.UpdateStatus(id, userId, TaskStatus.ToDo))
            {
                return HttpNotFound();
            }

            TempData["SuccessMessage"] = "Đã mở lại task.";
            return RedirectToAction("Index", new { projectId });
        }

        private ActionResult ChangeStatus(
            int id,
            int projectId,
            TaskStatus requiredCurrent,
            TaskStatus newStatus,
            string successMessage)
        {
            var userId = RequireUserId();
            var task = _taskRepository.GetByIdForUser(id, userId);
            if (task == null || task.ProjectId != projectId || task.Status != requiredCurrent)
            {
                return HttpNotFound();
            }

            if (!_taskRepository.UpdateStatus(id, userId, newStatus))
            {
                return HttpNotFound();
            }

            TempData["SuccessMessage"] = successMessage;
            return RedirectToAction("Index", new { projectId });
        }

        private static KanbanTaskCardViewModel MapToCard(Core.Entities.WorkTask task)
        {
            var today = DateTime.Today;
            return new KanbanTaskCardViewModel
            {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Priority = task.Priority,
                PriorityLabel = TaskEnumHelper.GetPriorityLabel(task.Priority),
                Status = task.Status,
                Deadline = task.Deadline,
                IsOverdue = task.Deadline.HasValue
                    && task.Deadline.Value.Date < today
                    && task.Status != TaskStatus.Done
            };
        }
    }
}
