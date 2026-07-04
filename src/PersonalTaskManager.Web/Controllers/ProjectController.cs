using System.Linq;
using System.Web.Mvc;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Repositories;
using PersonalTaskManager.Web.Models.Project;

namespace PersonalTaskManager.Web.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController()
        {
            _projectRepository = new ProjectRepository();
        }

        public ActionResult Index()
        {
            var userId = RequireUserId();
            var projects = _projectRepository.GetActiveByUserId(userId);

            var model = new ProjectListViewModel
            {
                Projects = projects.Select(p => new ProjectListItemViewModel
                {
                    ProjectId = p.ProjectId,
                    Name = p.Name,
                    Description = p.Description,
                    CreatedAt = p.CreatedAt
                }).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProjectFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = RequireUserId();
            _projectRepository.Create(userId, model.Name, model.Description);

            TempData["SuccessMessage"] = "Đã tạo dự án mới.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userId = RequireUserId();
            var project = _projectRepository.GetByIdForUser(id, userId);
            if (project == null)
            {
                return HttpNotFound();
            }

            var model = new ProjectFormViewModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectFormViewModel model)
        {
            if (!model.ProjectId.HasValue)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = RequireUserId();
            var updated = _projectRepository.Update(
                model.ProjectId.Value,
                userId,
                model.Name,
                model.Description);

            if (!updated)
            {
                return HttpNotFound();
            }

            TempData["SuccessMessage"] = "Đã cập nhật dự án.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var userId = RequireUserId();
            var deleted = _projectRepository.SoftDelete(id, userId);

            if (!deleted)
            {
                return HttpNotFound();
            }

            TempData["SuccessMessage"] = "Đã xóa dự án.";
            return RedirectToAction("Index");
        }
    }
}
