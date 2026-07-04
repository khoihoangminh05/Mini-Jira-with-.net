using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PersonalTaskManager.Core.Entities;

namespace PersonalTaskManager.Infrastructure.Data
{
    /// <summary>DbContext EF6 Code First — map entity sang bảng Oracle UPPER_CASE.</summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>Schema Oracle = user app (tránh EF mặc định "dbo" của SQL Server).</summary>
        private const string OracleSchema = "APP_USER";

        public ApplicationDbContext()
            : base("name=TaskManagerDb")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkTask> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(OracleSchema);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            ConfigureUser(modelBuilder);
            ConfigureProject(modelBuilder);
            ConfigureWorkTask(modelBuilder);
        }

        private static void ConfigureUser(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<User>();
            entity.ToTable("USERS", OracleSchema);
            entity.HasKey(u => u.UserId);
            entity.Property(u => u.UserId)
                .HasColumnName("USER_ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            entity.Property(u => u.Username).HasColumnName("USERNAME").IsRequired().HasMaxLength(50);
            entity.Property(u => u.Email).HasColumnName("EMAIL").IsRequired().HasMaxLength(100);
            entity.Property(u => u.PasswordHash).HasColumnName("PASSWORD_HASH").IsRequired().HasMaxLength(256);
            entity.Property(u => u.CreatedAt).HasColumnName("CREATED_AT");
        }

        private static void ConfigureProject(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Project>();
            entity.ToTable("PROJECTS", OracleSchema);
            entity.HasKey(p => p.ProjectId);
            entity.Property(p => p.ProjectId)
                .HasColumnName("PROJECT_ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            entity.Property(p => p.UserId).HasColumnName("USER_ID");
            entity.Property(p => p.Name).HasColumnName("NAME").IsRequired().HasMaxLength(200);
            entity.Property(p => p.Description).HasColumnName("DESCRIPTION").HasMaxLength(2000);
            entity.Property(p => p.CreatedAt).HasColumnName("CREATED_AT");
            entity.Property(p => p.IsDeleted).HasColumnName("IS_DELETED");

            entity.HasRequired(p => p.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
        }

        private static void ConfigureWorkTask(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<WorkTask>();
            entity.ToTable("TASKS", OracleSchema);
            entity.HasKey(t => t.TaskId);
            entity.Property(t => t.TaskId)
                .HasColumnName("TASK_ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            entity.Property(t => t.ProjectId).HasColumnName("PROJECT_ID");
            entity.Property(t => t.Title).HasColumnName("TITLE").IsRequired().HasMaxLength(200);
            entity.Property(t => t.Description).HasColumnName("DESCRIPTION");
            entity.Property(t => t.Priority).HasColumnName("PRIORITY");
            entity.Property(t => t.Status).HasColumnName("STATUS");
            entity.Property(t => t.Deadline).HasColumnName("DEADLINE");
            entity.Property(t => t.SortOrder).HasColumnName("SORT_ORDER");
            entity.Property(t => t.CreatedAt).HasColumnName("CREATED_AT");
            entity.Property(t => t.IsDeleted).HasColumnName("IS_DELETED");

            entity.HasRequired(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .WillCascadeOnDelete(false);
        }
    }
}
