using Domain.UserAggregate;

namespace Contracts.Users;

public record GetAllUsersResponse(List<UserResponse> Users);

public record UserResponse(Guid Id, string FirstName, string LastName, string Email);
