using System;
using System.Linq;
using PersonalTaskManager.Core.Entities;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Data;

namespace PersonalTaskManager.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public User GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            using (var context = new ApplicationDbContext())
            {
                var normalized = username.Trim();
                return context.Users
                    .FirstOrDefault(u => u.Username == normalized);
            }
        }

        public User GetByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            using (var context = new ApplicationDbContext())
            {
                var normalized = email.Trim().ToLowerInvariant();
                // Email luôn lưu lowercase — tránh ToLower() trong LINQ (Oracle EF không dịch tốt)
                return context.Users
                    .FirstOrDefault(u => u.Email == normalized);
            }
        }

        public bool UsernameExists(string username)
        {
            return GetByUsername(username) != null;
        }

        public bool EmailExists(string email)
        {
            return GetByEmail(email) != null;
        }

        public User Create(string username, string email, string passwordHash)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = new User
                {
                    Username = username.Trim(),
                    Email = email.Trim().ToLowerInvariant(),
                    PasswordHash = passwordHash,
                    CreatedAt = DateTime.Now
                };

                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }
        }
    }
}
