using TeachPlanner.Shared.Enums;
using TeachPlanner.Shared.Domain.PlannerTemplates;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Blazor.Common.Interfaces.Persistence;

public interface IYearDataRepository : IRepository<YearData> {
    Task<YearData?> GetByTeacherIdAndYear(TeacherId teacherId, int calendarYear, CancellationToken cancellationToken);
    Task SetInitialAccountDetails(Teacher teacher, List<YearLevelValue> yearLevelsTaught, DayPlanTemplate dayPlanTemplate,
               int calendarYear, CancellationToken cancellationToken);
    Task<IEnumerable<YearLevelValue>> GetYearLevelsTaught(TeacherId teacherId, int calendarYear, CancellationToken cancellationToken);
}