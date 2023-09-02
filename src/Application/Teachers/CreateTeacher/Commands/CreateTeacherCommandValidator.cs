using FluentValidation;

namespace TeachPlanner.Application.Teachers.CreateTeacher.Commands;

public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
{
    public CreateTeacherCommandValidator()
    {
        RuleFor(t => t.TeacherId).NotEmpty();
        RuleFor(t => t.FirstName).NotEmpty();
        RuleFor(t => t.LastName).NotEmpty();
        RuleFor(t => t.Email).NotEmpty().EmailAddress();
        RuleFor(t => t.Password).NotEmpty();
    }
}
