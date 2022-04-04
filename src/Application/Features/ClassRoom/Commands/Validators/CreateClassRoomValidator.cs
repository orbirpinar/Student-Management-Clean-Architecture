using Application.Features.ClassRoom.Dtos;
using FluentValidation;

namespace Application.Features.ClassRoom.Commands.Validators
{
    public class CreateClassRoomValidator: AbstractValidator<CreateClassRoomCommand>
    {
        public CreateClassRoomValidator()
        {
            RuleFor(c => c.Grade)
                .NotEmpty()
                .Must(grade => grade is > 1 and < 13).WithMessage("Grade must be between and 12");
            RuleFor(c => c.Group)
                .NotEmpty()
                .Matches("^[a-zA-Z]$").WithMessage("Class group must be letters");
        }
    }
}