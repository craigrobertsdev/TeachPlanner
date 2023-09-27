using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Infrastructure.Persistence.Repositories;
public class YearDataRepository : IYearDataRepository
{
    public YearDataRepository()
    {
    }

    public Task<YearData?> GetByTeacherAndYear(Guid teacherId, int calendarYear, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
