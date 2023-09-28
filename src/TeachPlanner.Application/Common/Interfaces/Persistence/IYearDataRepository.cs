using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface IYearDataRepository : IRepository<YearData>
{
    Task<YearData?> GetByTeacherAndYear(Guid teacherId, int calendarYear, CancellationToken cancellationToken);
}
