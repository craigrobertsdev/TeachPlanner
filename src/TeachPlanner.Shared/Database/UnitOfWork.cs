using TeachPlanner.Blazor.Common.Interfaces.Persistence;

namespace TeachPlanner.Blazor.Database;

internal sealed class UnitOfWork : IUnitOfWork {
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context) {
        _context = context;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) {
        return _context.SaveChangesAsync(cancellationToken);
    }
}