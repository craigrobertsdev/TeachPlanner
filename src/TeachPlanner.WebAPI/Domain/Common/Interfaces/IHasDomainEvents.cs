using TeachPlanner.Api.Domain.Common.Primatives;

namespace TeachPlanner.Api.Domain.Common.Interfaces;
public interface IHasDomainEvents
{
    public IReadOnlyList<DomainEvent> DomainEvents { get; }
    public void ClearDomainEvents();
}
