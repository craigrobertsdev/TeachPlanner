using Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery()
    : IRequest<ErrorOr<GetAllUsersResult>>;
