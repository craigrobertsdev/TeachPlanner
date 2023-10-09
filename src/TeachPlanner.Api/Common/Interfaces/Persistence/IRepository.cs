using TeachPlanner.Api.Domain.Common.Interfaces;

namespace TeachPlanner.Api.Common.Interfaces.Persistence;
public interface IRepository<T> where T : class, IAggregateRoot
{
}
