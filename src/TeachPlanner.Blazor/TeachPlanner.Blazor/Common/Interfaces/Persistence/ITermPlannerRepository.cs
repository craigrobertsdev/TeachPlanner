using TeachPlanner.Shared.Domain.TermPlanners;
using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Blazor.Common.Interfaces.Persistence;

public interface ITermPlannerRepository {
    Task<TermPlanner?> GetById(TermPlannerId id, CancellationToken cancellationToken);

    Task<TermPlanner?> GetByYearDataIdAndYear(YearDataId yearDataId, int calendarYear,
        CancellationToken cancellationToken);

    void Add(TermPlanner termPlanner);
    Task Delete(TermPlannerId id, CancellationToken cancellationToken);
}