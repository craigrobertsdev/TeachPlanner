using TeachPlanner.Api.Domain.TermPlanners;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;
public interface ITermPlannerRepository
{
    Task<TermPlanner?> GetById(TermPlannerId id, CancellationToken cancellationToken);
    void Add(TermPlanner termPlanner);
    Task Delete(TermPlannerId id, CancellationToken cancellationToken);
}
