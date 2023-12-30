using Microsoft.EntityFrameworkCore;
using TeachPlanner.Shared.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Domain.Users;

namespace TeachPlanner.Shared.Database.Repositories;

public class UserRepository : IUserRepository {
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) {
        _context = context;
    }

    public void Add(User user) {
        //_context.Users.Add(user);
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken) {
        //return await _context.Users
        //    .Where(u => u.Email == email)
        //    .FirstOrDefaultAsync(cancellationToken);
        throw new NotImplementedException();
    }
}