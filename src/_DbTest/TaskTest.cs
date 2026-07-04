using System;
using PersonalTaskManager.Core.Enums;
using PersonalTaskManager.Infrastructure.Repositories;

class Program {
    static void Main() {
        try {
            var projectRepo = new ProjectRepository();
            var taskRepo = new TaskRepository();
            var userId = 1;
            var projects = projectRepo.GetActiveByUserId(userId);
            if (projects.Count == 0) {
                var p = projectRepo.Create(userId, "Phase4 Test", "desc");
                projects = projectRepo.GetActiveByUserId(userId);
            }
            var projectId = projects[0].ProjectId;
            var t = taskRepo.Create(projectId, userId, "Task test", "Mo ta", TaskPriority.High, TaskStatus.ToDo, DateTime.Today.AddDays(7));
            Console.WriteLine("Created TaskId=" + t.TaskId + " ProjectId=" + t.ProjectId);
            Console.WriteLine("List count=" + taskRepo.GetActiveByProjectId(projectId, userId).Count);
            var ok = taskRepo.Update(t.TaskId, userId, "Task updated", "New desc", TaskPriority.Low, TaskStatus.InProgress, null);
            Console.WriteLine("Update=" + ok);
            var del = taskRepo.SoftDelete(t.TaskId, userId);
            Console.WriteLine("Delete=" + del);
            Console.WriteLine("Cross user=" + (taskRepo.GetByIdForUser(t.TaskId, 999) == null));
        } catch (Exception ex) {
            Console.WriteLine("ERROR: " + ex.Message);
            if (ex.InnerException != null) Console.WriteLine("INNER: " + ex.InnerException.Message);
        }
    }
}
