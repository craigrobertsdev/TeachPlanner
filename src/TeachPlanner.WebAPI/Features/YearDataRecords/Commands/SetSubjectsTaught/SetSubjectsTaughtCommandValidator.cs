using FluentValidation;

namespace TeachPlanner.Api.Features.YearDataRecords.Commands.SetSubjectsTaught;
public class SetSubjectsTaughtCommandValidator : AbstractValidator<SetSubjectsTaughtCommand>
{
    public SetSubjectsTaughtCommandValidator()
    {
        RuleFor(x => x.SubjectIds)
            .NotEmpty()
            .WithMessage("At least one subject must be provided");
    }
}
