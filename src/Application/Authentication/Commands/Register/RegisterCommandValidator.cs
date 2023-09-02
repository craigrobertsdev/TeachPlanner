using FluentValidation;
using TeachPlanner.Application.Common.Interfaces.Persistence;

namespace TeachPlanner.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(ITeacherRepository teacherRepository)
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        // TODO add better password validation behaviour

        RuleFor(u => u.Email).NotEmpty().EmailAddress();
    }
}
