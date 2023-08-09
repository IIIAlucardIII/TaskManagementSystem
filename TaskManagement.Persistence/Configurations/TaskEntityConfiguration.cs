using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagment.Domain;

namespace TaskManagement.Persistence.Configurations
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder
                .HasKey(t => t.Id);
            builder.HasOne(t => t.CreatedBy).WithMany(u => u.Tasks).HasForeignKey(t => t.CreatedById);
        }
    }
}
