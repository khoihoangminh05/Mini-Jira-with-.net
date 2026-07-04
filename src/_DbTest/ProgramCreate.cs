using System;
using PersonalTaskManager.Infrastructure.Repositories;
using PersonalTaskManager.Infrastructure.Security;

class Program {
    static void Main() {
        try {
            var repo = new UserRepository();
            var name = "test_" + DateTime.Now.Ticks;
            var hash = PasswordHasher.HashPassword("Test1234");
            var user = repo.Create(name, name + "@test.local", hash);
            Console.WriteLine("Created UserId=" + user.UserId + " Username=" + user.Username);
            var found = repo.GetByUsername(name);
            Console.WriteLine("Found again: " + (found != null));
        } catch (Exception ex) {
            Console.WriteLine("ERROR: " + ex.Message);
            if (ex.InnerException != null) Console.WriteLine("INNER: " + ex.InnerException.Message);
        }
    }
}
