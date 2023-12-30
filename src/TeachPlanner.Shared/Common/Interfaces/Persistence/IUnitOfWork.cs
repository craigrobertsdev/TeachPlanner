namespace TeachPlanner.Shared.Common.Interfaces.Persistence;

public interface IUnitOfWork {
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}