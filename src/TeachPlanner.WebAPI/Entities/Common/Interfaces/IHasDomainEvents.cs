using TeachPlanner.Api.Entities.Common.Primatives;

namespace TeachPlanner.Api.Entities.Common.Interfaces;
public interface IHasDomainEvents
{
    public IReadOnlyList<DomainEvent> DomainEvents { get; }
    public void ClearDomainEvents();
}
