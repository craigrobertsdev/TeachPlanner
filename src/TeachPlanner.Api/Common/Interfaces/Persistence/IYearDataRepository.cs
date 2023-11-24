using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface IYearDataRepository : IRepository<YearData>
{
    Task<YearData?> GetByTeacherIdAndYear(TeacherId teacherId, int calendarYear, CancellationToken cancellationToken);
    Task SetInitialAccountDetails(Teacher teacher, List<string> yearLevelsTaught, DayPlanTemplate dayPlanTemplate,
               int calendarYear, CancellationToken cancellationToken);
}