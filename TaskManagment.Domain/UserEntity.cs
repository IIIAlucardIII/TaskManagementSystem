using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagment.Domain
{
    public class UserEntity: IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        public ICollection<TaskEntity> Tasks { get; set; }
    }
}
