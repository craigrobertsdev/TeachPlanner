using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Domain.Users;

namespace TeachPlanner.Api.Database.QueryExtensions;

public static class AuthenticationQueries
{
    public static async Task<User?> GetUserByEmail(this ApplicationDbContext context, string email,
        CancellationToken cancellationToken)
    {
        return await context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }
}