using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.YearDataRecords;
public record YearDataCreatedDomainEvent(Guid Id, YearDataId YearDataId) : DomainEvent(Id);
