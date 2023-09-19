using FluentValidation;
using TaskManagement.Web.Models;

namespace TaskManagement.Web.Validation
{
    public class EditTaskModelValidator : AbstractValidator<EditTaskModel>
    {
        public EditTaskModelValidator()
        {
            RuleFor(t => t.Name).MinimumLength(3).WithMessage("The name should be more then 3 characters long.");
        }
    }
}