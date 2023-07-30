using Application.Common.Interfaces.Persistence;
using Domain.UserAggregate;
using Infrastructure.Persistence.DbContexts;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);

    }

    public List<User?> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public bool IsEmailUnique(string email)
    {
        return !_context.Users.Any(u => u.Email == email);
    }
}
