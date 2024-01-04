using Microsoft.EntityFrameworkCore;
using TeachPlanner.Shared.Domain.Common.Enums;
using TeachPlanner.Shared.Domain.PlannerTemplates;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.YearDataRecords;
using TeachPlanner.Shared.Common.Interfaces.Persistence;

namespace TeachPlanner.Shared.Database.Repositories;

public class YearDataRepository : IYearDataRepository {
    private readonly ApplicationDbContext _context;

    public YearDataRepository(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<YearData?> GetByTeacherIdAndYear(TeacherId teacherId, int calendarYear,
        CancellationToken cancellationToken) {
        return await _context.YearData
            .Where(yd => yd.CalendarYear == calendarYear)
            .Include(yd => yd.Subjects)
            .Include(yd => yd.Students)
            .Include(yd => yd.WeekPlanners)
            .Include(yd => yd.LessonPlans)
            .Include(yd => yd.DayPlanTemplate)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task SetInitialAccountDetails(Teacher teacher, List<YearLevelValue> yearLevelsTaught, DayPlanTemplate dayPlanTemplate,
               int calendarYear, CancellationToken cancellationToken) {
        var yearData = await _context.YearData
            .Where(yd => yd.CalendarYear == calendarYear)
            .Include(yd => yd.DayPlanTemplate)
            .FirstAsync(cancellationToken);

        //if (yearData is null)
        //{
        //    yearData = YearData.Create(teacher.Id, calendarYear, dayPlanTemplate, yearLevelsTaught);
        //    teacher.AddYearData(YearDataEntry.Create(calendarYear, yearData.Id));
        //    _context.YearData.Add(yearData);
        //    return;
        //}

        yearData.SetDayPlanTemplate(dayPlanTemplate);
        yearData.SetYearLevelsTaught(yearLevelsTaught);
    }

    public async Task<IEnumerable<YearLevelValue>> GetYearLevelsTaught(TeacherId teacherId, int calendarYear, CancellationToken cancellationToken) {
        var yearData = await _context.YearData
            .Where(yd => yd.CalendarYear == calendarYear)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return yearData?.YearLevelsTaught ?? new List<YearLevelValue>();
    }
}