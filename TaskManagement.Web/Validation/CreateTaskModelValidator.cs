using FluentValidation;
using TaskManagement.Web.Models;

namespace TaskManagement.Web.Validation
{
    public class CreateTaskModelValidator : AbstractValidator<CreateTaskModel>
    {
        public CreateTaskModelValidator()
        {
            RuleFor(t => t.Name).MinimumLength(3).WithMessage("The name should be more then 3 characters long.");
        }
    }
}