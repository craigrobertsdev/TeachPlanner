using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;

namespace TeachPlanner.Api.Database.QueryExtensions;

public static class TeacherQueries
{
    public static async Task<Teacher?> GetTeacherByUserId(this ApplicationDbContext context, UserId userId)
    {
        return await context.Teachers
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.UserId == userId);
    }
    public static async Task<Teacher?> GetByEmail(this ApplicationDbContext context, string email, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new InvalidCredentialsException();
        }

        return await context.Teachers
            .Where(t => t.UserId == user.Id)
            .Include(t => t.Assessments)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
