using TeachPlanner.Shared.Domain.Common.Enums;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface IYearDataRepository : IRepository<YearData> {
    Task<YearData?> GetByTeacherIdAndYear(TeacherId teacherId, int calendarYear, CancellationToken cancellationToken);
    Task SetInitialAccountDetails(Teacher teacher, List<YearLevelValue> yearLevelsTaught, DayPlanTemplate dayPlanTemplate,
               int calendarYear, CancellationToken cancellationToken);
    Task<IEnumerable<YearLevelValue>> GetYearLevelsTaught(TeacherId teacherId, int calendarYear, CancellationToken cancellationToken);
}