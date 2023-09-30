using TeachPlanner.Api.Entities.Teachers;
using TeachPlanner.Api.Entities.YearDataRecords;
using TeachPlanner.Api.Features.Common.Interfaces.Persistence;

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
