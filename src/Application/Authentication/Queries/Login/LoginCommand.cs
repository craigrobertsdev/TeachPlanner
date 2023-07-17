using Application.Authentication.Common;
using Application.Common;
using Application.Common.Exceptions;
using MediatR;

namespace Application.Authentication.Queries.Login;

public record LoginCommand(string Email, string Password) : IRequest<Either<AuthenticationResult, AuthenticationException>>;
