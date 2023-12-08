namespace TeachPlanner.Api.Common.Interfaces.Persistence;

public interface IUnitOfWork {
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}