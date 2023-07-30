using Domain.UserAggregate;

namespace Application.Users.Common;

public record GetAllUsersResult(List<User> Users);
