using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface IYearDataRepository
{
    Task<YearData?> GetByTeacherAndYear(Guid teacherId, int calendarYear, CancellationToken cancellationToken);
}
