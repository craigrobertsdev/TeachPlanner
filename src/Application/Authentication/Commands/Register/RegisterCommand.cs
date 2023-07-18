using Application.Authentication.Common;
using Application.Common;
using Application.Common.Exceptions.Authentication;
using MediatR;

namespace Application.Authentication.Commands.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Either<AuthenticationResult, DuplicateUserException>>;
