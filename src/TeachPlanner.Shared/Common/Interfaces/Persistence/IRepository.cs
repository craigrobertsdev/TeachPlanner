using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Shared.Common.Interfaces.Persistence;

public interface IRepository<T> where T : class, IAggregateRoot {
}