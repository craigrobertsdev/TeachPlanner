using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.YearDataRecords;
public record YearDataCreatedDomainEvent(Guid Id, Guid YearDataId) : DomainEvent(Id);
