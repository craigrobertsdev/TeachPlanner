using TeachPlanner.Domain.Common.Interfaces;

namespace TeachPlanner.Application.Common.Interfaces.Persistence;
public interface IRepository<T> where T : class, IAggregateRoot
{
}
