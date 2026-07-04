using PersonalTaskManager.Core.Entities;

namespace PersonalTaskManager.Core.Interfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);

        User GetByEmail(string email);

        bool UsernameExists(string username);

        bool EmailExists(string email);

        User Create(string username, string email, string passwordHash);
    }
}
