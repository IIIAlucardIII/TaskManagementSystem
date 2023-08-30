using TaskManagment.Domain.Enums;

namespace TaskManagement.Web.Models
{
    public class TaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public TaskPriority Priority { get; set; }
    }
}
