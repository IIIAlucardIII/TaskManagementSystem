using TaskManagment.Domain.Enums;

namespace TaskManagement.Web.Models
{
    public class CreateTaskModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
