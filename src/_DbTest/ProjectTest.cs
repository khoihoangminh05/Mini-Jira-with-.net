using System;
using PersonalTaskManager.Infrastructure.Repositories;

class Program {
    static void Main() {
        try {
            var repo = new ProjectRepository();
            var userId = 1;
            var p = repo.Create(userId, "Hoc .NET", "Phase 3 test");
            Console.WriteLine("Created ProjectId=" + p.ProjectId);
            var list = repo.GetActiveByUserId(userId);
            Console.WriteLine("Count=" + list.Count);
            var ok = repo.Update(p.ProjectId, userId, "Hoc .NET MVC", "Updated");
            Console.WriteLine("Update=" + ok);
            var del = repo.SoftDelete(p.ProjectId, userId);
            Console.WriteLine("Delete=" + del);
            Console.WriteLine("After delete Count=" + repo.GetActiveByUserId(userId).Count);
            var cross = repo.GetByIdForUser(p.ProjectId, 999);
            Console.WriteLine("Cross user null=" + (cross == null));
        } catch (Exception ex) {
            Console.WriteLine("ERROR: " + ex.Message);
            if (ex.InnerException != null) Console.WriteLine("INNER: " + ex.InnerException.Message);
        }
    }
}
