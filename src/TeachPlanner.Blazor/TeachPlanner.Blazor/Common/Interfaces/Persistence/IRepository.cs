using TeachPlanner.Shared.Domain.Common.Interfaces;

namespace TeachPlanner.Blazor.Common.Interfaces.Persistence;

public interface IRepository<T> where T : class, IAggregateRoot {
}