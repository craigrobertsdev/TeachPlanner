using FluentValidation;

namespace Application.Teachers.CreateTeacher.Commands;

public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
{
    public CreateTeacherCommandValidator()
    {
        RuleFor(t => t.UserId).NotEmpty();
    }
}
