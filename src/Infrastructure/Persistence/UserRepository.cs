using Application.Common.Interfaces.Persistence;
using Domain.UserAggregate;

namespace Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return User.Create("Craig", "Roberts", email, "password123");
    }
}
