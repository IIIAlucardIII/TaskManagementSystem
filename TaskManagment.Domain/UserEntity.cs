using Microsoft.AspNetCore.Identity;

namespace TaskManagment.Domain
{
    public class UserEntity: IdentityUser
    {
        public ICollection<TaskEntity> Tasks { get; set; }
    }
}
