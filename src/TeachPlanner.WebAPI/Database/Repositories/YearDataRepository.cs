using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Database.Repositories;
public class YearDataRepository : IYearDataRepository
{
    public YearDataRepository()
    {
    }

    public Task<YearData?> GetByTeacherAndYear(TeacherId teacherId, int calendarYear, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
