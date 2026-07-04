using System;
using System.Collections.Generic;
using System.Linq;
using PersonalTaskManager.Core.Entities;
using PersonalTaskManager.Core.Interfaces;
using PersonalTaskManager.Infrastructure.Data;

namespace PersonalTaskManager.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public IList<Project> GetActiveByUserId(int userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Projects
                    .Where(p => p.UserId == userId && !p.IsDeleted)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToList();
            }
        }

        public Project GetByIdForUser(int projectId, int userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Projects
                    .FirstOrDefault(p => p.ProjectId == projectId && p.UserId == userId && !p.IsDeleted);
            }
        }

        public Project Create(int userId, string name, string description)
        {
            using (var context = new ApplicationDbContext())
            {
                var project = new Project
                {
                    UserId = userId,
                    Name = name.Trim(),
                    Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
                    CreatedAt = DateTime.Now,
                    IsDeleted = false
                };

                context.Projects.Add(project);
                context.SaveChanges();
                return project;
            }
        }

        public bool Update(int projectId, int userId, string name, string description)
        {
            using (var context = new ApplicationDbContext())
            {
                var project = context.Projects
                    .FirstOrDefault(p => p.ProjectId == projectId && p.UserId == userId && !p.IsDeleted);

                if (project == null)
                {
                    return false;
                }

                project.Name = name.Trim();
                project.Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
                context.SaveChanges();
                return true;
            }
        }

        public bool SoftDelete(int projectId, int userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var project = context.Projects
                    .FirstOrDefault(p => p.ProjectId == projectId && p.UserId == userId && !p.IsDeleted);

                if (project == null)
                {
                    return false;
                }

                project.IsDeleted = true;
                context.SaveChanges();
                return true;
            }
        }
    }
}
