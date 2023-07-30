using Application.Common.Interfaces.Persistence;
using Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Application.Users.Queries.GetAllUsers;
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ErrorOr<GetAllUsersResult>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<GetAllUsersResult>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userRepository.GetAllUsers();

        return new GetAllUsersResult(users);
    }
}
