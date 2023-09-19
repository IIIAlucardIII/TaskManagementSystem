using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Domain.Enums
{
    public enum Status
    {
        [Display(Name = "Not Started")]
        NotStarted,
        [Display(Name = "In progress")]
        InProgress,
        [Display(Name = "Completed")]
        Completed
    }
}
