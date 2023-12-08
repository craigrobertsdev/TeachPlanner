using Microsoft.EntityFrameworkCore;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Database.QueryExtensions;

public static class YearDataQueries {
    public static async Task<YearData?> GetYearData(this ApplicationDbContext context, TeacherId teacherId,
        int calendarYear, CancellationToken cancellationToken) {
        return await context.YearData
            .Where(yd => yd.TeacherId == teacherId)
            .Where(yd => yd.CalendarYear == calendarYear)
            .Include(yd => yd.Subjects)
            .FirstOrDefaultAsync(cancellationToken);
    }
}