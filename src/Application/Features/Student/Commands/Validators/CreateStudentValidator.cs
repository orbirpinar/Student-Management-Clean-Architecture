using FluentValidation;

namespace Application.Features.Student.Commands.Validators
{
    public class CreateStudentValidator: AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentValidator()
        {
            RuleFor(s => s.FirstName)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(s => s.LastName)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(s => s.Gender)
                .IsInEnum();
        }
    }
}