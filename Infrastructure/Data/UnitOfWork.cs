using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UnitOfWork : IDisposable {
    private ApplicationDbContext _context;
    private bool _disposed = false;
    private Dictionary<string, object> Repositories { get; }


    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public Repository<TEntity> Repository<TEntity>() where TEntity : class {
        return new Repository<TEntity>(_context);
    }
    
    public void Dispose() {
        throw new NotImplementedException();
    }
}
