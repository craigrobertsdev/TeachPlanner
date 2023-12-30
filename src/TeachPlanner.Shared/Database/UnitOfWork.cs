using TeachPlanner.Shared.Common.Interfaces.Persistence;

namespace TeachPlanner.Shared.Database;

public sealed class UnitOfWork : IUnitOfWork {
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context) {
        _context = context;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) {
        return _context.SaveChangesAsync(cancellationToken);
    }
}