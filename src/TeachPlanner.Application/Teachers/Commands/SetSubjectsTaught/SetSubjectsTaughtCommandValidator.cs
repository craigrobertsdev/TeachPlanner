using FluentValidation;

namespace TeachPlanner.Application.Teachers.Commands.SetSubjectsTaught;
public class SetSubjectsTaughtCommandValidator : AbstractValidator<SetSubjectsTaughtCommand>
{
    public SetSubjectsTaughtCommandValidator()
    {
        RuleFor(x => x.SubjectNames)
            .NotEmpty()
            .WithMessage("At least one subject must be provided");
    }
}
