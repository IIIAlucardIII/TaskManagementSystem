using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Persistence.Configurations;
using TaskManagment.Domain;

namespace TaskManagement.Persistence
{
    public class TaskManagementDbContext : IdentityDbContext<UserEntity>
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
            : base(options)
        {
           
        }
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new TaskEntityConfiguration().Configure(modelBuilder.Entity<TaskEntity>());
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
