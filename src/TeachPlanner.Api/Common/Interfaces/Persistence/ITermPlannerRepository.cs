using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface ITermPlannerRepository {
    Task<TermPlanner?> GetById(TermPlannerId id, CancellationToken cancellationToken);

    Task<TermPlanner?> GetByYearDataIdAndYear(YearDataId yearDataId, int calendarYear,
        CancellationToken cancellationToken);

    void Add(TermPlanner termPlanner);
    Task Delete(TermPlannerId id, CancellationToken cancellationToken);
}