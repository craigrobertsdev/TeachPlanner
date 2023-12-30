using Microsoft.EntityFrameworkCore;
using TeachPlanner.Shared.Database;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Shared.Database.QueryExtensions;

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