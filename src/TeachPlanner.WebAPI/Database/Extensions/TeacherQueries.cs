using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Database.Extensions;

public static class TeacherQueries
{
    public static async Task<Teacher?> GetTeacherByUserId(this ApplicationDbContext context, Guid userId)
    {
        return await context.Teachers
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.UserId == userId);
    }
}
