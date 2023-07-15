using Domain.Entities;

namespace Domain.Abstractions;

public interface IUnitOfWork : IDisposable {
    IRepository<Subject> SubjectRepository { get; }
    void Save();
}
