using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagment.Domain
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsEdited { get; set; }

        public UserEntity CreatedBy { get; set; }
        public string CreatedById { get; set; }

    }
}
