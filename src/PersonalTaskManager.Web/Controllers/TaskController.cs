using System.Linq;
using System.Web.Mvc;
using PersonalTaskManager.Core.Enums;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Repositories;
using PersonalTaskManager.Web.Helpers;
using PersonalTaskManager.Web.Models.Kanban;
using PersonalTaskManager.Web.Models.Task;

namespace PersonalTaskManager.Web.Controllers
{
    [Authorize]
    public class TaskController : BaseController
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public TaskController()
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
            var model = new TaskListViewModel
            {
                ProjectId = project.ProjectId,
                ProjectName = project.Name,
                Tasks = tasks.Select(t => new TaskListItemViewModel
                {
                    TaskId = t.TaskId,
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority,
                    PriorityLabel = TaskEnumHelper.GetPriorityLabel(t.Priority),
                    Status = t.Status,
                    StatusLabel = TaskEnumHelper.GetStatusLabel(t.Status),
                    Deadline = t.Deadline,
                    CreatedAt = t.CreatedAt
                }).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int projectId)
        {
            var userId = RequireUserId();
            var project = _projectRepository.GetByIdForUser(projectId, userId);
            if (project == null)
            {
                return HttpNotFound();
            }

            ViewBag.PriorityList = TaskEnumHelper.GetPrioritySelectList();
            ViewBag.StatusList = TaskEnumHelper.GetStatusSelectList();

            return View(new TaskFormViewModel
            {
                ProjectId = project.ProjectId,
                ProjectName = project.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PriorityList = TaskEnumHelper.GetPrioritySelectList(model.Priority);
                ViewBag.StatusList = TaskEnumHelper.GetStatusSelectList(model.Status);
                return View(model);
            }

            var userId = RequireUserId();
            var project = _projectRepository.GetByIdForUser(model.ProjectId, userId);
            if (project == null)
            {
                return HttpNotFound();
            }

            var task = _taskRepository.Create(
                model.ProjectId,
                userId,
                model.Title,
                model.Description,
                model.Priority,
                model.Status,
                model.Deadline);

            if (task == null)
            {
                return HttpNotFound();
            }

            ActivityLogger.TryLog(
                userId,
                model.ProjectId,
                task.TaskId,
                "TaskCreated",
                string.Format("Tạo task \"{0}\"", task.Title));

            TempData["SuccessMessage"] = "Đã tạo task mới.";
            return RedirectToAction("Index", new { projectId = model.ProjectId });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userId = RequireUserId();
            var task = _taskRepository.GetByIdForUser(id, userId);
            if (task == null)
            {
                return HttpNotFound();
            }

            var project = _projectRepository.GetByIdForUser(task.ProjectId, userId);
            if (project == null)
            {
                return HttpNotFound();
            }

            ViewBag.PriorityList = TaskEnumHelper.GetPrioritySelectList(task.Priority);
            ViewBag.StatusList = TaskEnumHelper.GetStatusSelectList(task.Status);

            var model = new TaskFormViewModel
            {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,
                ProjectName = project.Name,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                Deadline = task.Deadline
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskFormViewModel model)
        {
            if (!model.TaskId.HasValue)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.PriorityList = TaskEnumHelper.GetPrioritySelectList(model.Priority);
                ViewBag.StatusList = TaskEnumHelper.GetStatusSelectList(model.Status);
                return View(model);
            }

            var userId = RequireUserId();
            var updated = _taskRepository.Update(
                model.TaskId.Value,
                userId,
                model.Title,
                model.Description,
                model.Priority,
                model.Status,
                model.Deadline);

            if (!updated)
            {
                return HttpNotFound();
            }

            TempData["SuccessMessage"] = "Đã cập nhật task.";
            return RedirectToAction("Index", new { projectId = model.ProjectId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int projectId)
        {
            var userId = RequireUserId();
            var deleted = _taskRepository.SoftDelete(id, userId);

            if (!deleted)
            {
                return HttpNotFound();
            }

            TempData["SuccessMessage"] = "Đã xóa task.";
            return RedirectToAction("Index", new { projectId });
        }

        /// <summary>Ajax — cập nhật trạng thái task (Phase 7).</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateStatus(int id, int projectId, string action)
        {
            var userId = RequireUserId();
            TaskStatus newStatus;
            string message;

            if (!TaskStatusTransition.TryApply(_taskRepository, id, userId, projectId, action, out newStatus, out message))
            {
                return Json(new { success = false, message });
            }

            var task = _taskRepository.GetByIdForUser(id, userId);
            var logMessage = task != null
                ? string.Format("Task \"{0}\": {1}", task.Title, message)
                : message;
            ActivityLogger.TryLog(userId, projectId, id, "StatusChange", logMessage);

            return Json(new
            {
                success = true,
                message,
                taskId = id,
                newStatus = (int)newStatus,
                columnKey = TaskCardMapper.GetColumnKey(newStatus),
                activityLog = new { message = logMessage, timeLabel = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") }
            });
        }

        /// <summary>Partial card sau Ajax — refresh nút theo status mới.</summary>
        [HttpGet]
        public ActionResult Card(int id)
        {
            var userId = RequireUserId();
            var task = _taskRepository.GetByIdForUser(id, userId);
            if (task == null)
            {
                return HttpNotFound();
            }

            return PartialView("_TaskCard", TaskCardMapper.FromEntity(task));
        }

        /// <summary>Drag-drop — đổi cột + sort order (Phase 8). Cho phép kéo trực tiếp giữa mọi cột.</summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Move(int taskId, int projectId, int newStatus, int newSortOrder)
        {
            if (!System.Enum.IsDefined(typeof(TaskStatus), newStatus))
            {
                return Json(new { success = false, message = "Trạng thái không hợp lệ." });
            }

            var userId = RequireUserId();
            var status = (TaskStatus)newStatus;

            if (!_taskRepository.MoveTask(taskId, userId, projectId, status, newSortOrder))
            {
                return Json(new { success = false, message = "Không thể di chuyển task." });
            }

            var task = _taskRepository.GetByIdForUser(taskId, userId);
            var statusLabel = TaskEnumHelper.GetStatusLabel(status);
            var logMessage = task != null
                ? string.Format("Kéo task \"{0}\" → {1}", task.Title, statusLabel)
                : string.Format("Di chuyển task → {0}", statusLabel);
            ActivityLogger.TryLog(userId, projectId, taskId, "TaskMoved", logMessage);

            return Json(new
            {
                success = true,
                message = "Đã cập nhật vị trí task.",
                taskId,
                newStatus,
                columnKey = TaskCardMapper.GetColumnKey(status),
                activityLog = new { message = logMessage, timeLabel = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") }
            });
        }
    }
}
