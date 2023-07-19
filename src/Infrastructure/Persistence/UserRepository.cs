using Application.Common.Interfaces.Persistence;
using Domain.UserAggregate;

namespace Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();
    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);

    }

    public bool IsEmailUnique(string email)
    {
        return !_users.Any(u => u.Email == email);
    }
}
