using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
            modelBuilder.Entity<TaskEntity>().HasKey(t => t.Id);
            modelBuilder.Entity<TaskEntity>().HasOne(t => t.CreatedBy).WithMany(u => u.Tasks).HasForeignKey(t => t.CreatedById);
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
