namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
