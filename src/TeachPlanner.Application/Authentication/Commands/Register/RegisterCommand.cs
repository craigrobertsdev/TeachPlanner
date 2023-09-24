﻿using MediatR;
using TeachPlanner.Application.Authentication.Common;

namespace TeachPlanner.Application.Authentication.Commands.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password, string ConfirmedPassword)
    : IRequest<AuthenticationResult>;
