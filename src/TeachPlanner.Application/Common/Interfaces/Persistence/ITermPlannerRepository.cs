using TeachPlanner.Domain.TermPlanners;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface ITermPlannerRepository
{
    Task<TermPlanner?> GetById(Guid id, CancellationToken cancellationToken);
    void Add(TermPlanner termPlanner);
    Task Delete(Guid id, CancellationToken cancellationToken);
}
