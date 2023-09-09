using FluentValidation;
using TaskManagement.Web.Models;

namespace TaskManagement.Web.Validation
{
    public class TaskModelValidator : AbstractValidator<TaskModel>
    {
        public TaskModelValidator()
        {
            RuleFor(t => t.Name).MinimumLength(10).WithMessage("The name should be more then 10 characters long.");
        }
    }
}
