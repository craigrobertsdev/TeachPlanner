using Microsoft.EntityFrameworkCore;
using TeachPlanner.Shared.Common.Exceptions;
using TeachPlanner.Shared.Database;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.Users;

namespace TeachPlanner.Shared.Database.QueryExtensions;

public static class TeacherQueries {
    public static async Task<Teacher?> GetTeacherByUserId(this ApplicationDbContext context, string userId) {
        return await context.Teachers
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.UserId == userId);
    }

    public static async Task<Teacher?> GetTeacherByEmail(this ApplicationDbContext context, string email,
        CancellationToken cancellationToken) {
        var user = await context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null) throw new InvalidCredentialsException();

        return await context.Teachers
            .Where(t => t.UserId == user.Id)
            .Include(t => t.Assessments)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public static async Task<Teacher?> GetTeacherById(this ApplicationDbContext context, TeacherId teacherId,
        CancellationToken cancellationToken) {
        return await context.Teachers
            .AsNoTracking()
            .Where(t => t.Id == teacherId)
            .Include(t => t.Assessments)
            .FirstOrDefaultAsync(cancellationToken);
    }
}