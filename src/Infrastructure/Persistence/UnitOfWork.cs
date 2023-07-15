using Domain.Abstractions;
using Domain.Entities;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork {
    private readonly ApplicationDbContext _context;
    private bool _disposed = false;
    private IRepository<Subject> _subjectRepository;

    public UnitOfWork(ApplicationDbContext context) {
        _context = context;
    }

    public IRepository<Subject> SubjectRepository {
        get {
            if (_subjectRepository == null) {
                _subjectRepository = new Repository<Subject>(_context);
            }
            return _subjectRepository;
        }
    }

    protected virtual void Dispose(bool disposing) {
        if (!_disposed) {
            if (disposing) {
                _context?.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Save() {
        _context.SaveChanges();
    }
}
