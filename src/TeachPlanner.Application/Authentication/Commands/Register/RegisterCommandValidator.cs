﻿using FluentValidation;

namespace TeachPlanner.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        // TODO add better password validation behaviour

        RuleFor(u => u.Email).NotEmpty().EmailAddress();
    }
}
