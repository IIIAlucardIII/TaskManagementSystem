using TaskManagment.Domain.Enums;

namespace TaskManagment.Domain
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public TaskPriority Priority { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditedAt { get; set; }

        public UserEntity CreatedBy { get; set; }
        public string CreatedById { get; set; }

    }
}
