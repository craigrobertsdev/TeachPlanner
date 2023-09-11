using MediatR;
using TeachPlanner.Application.Authentication.Common;

namespace TeachPlanner.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password)
    : IRequest<AuthenticationResult>;
