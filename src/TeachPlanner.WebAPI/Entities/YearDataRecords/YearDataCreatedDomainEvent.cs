using TeachPlanner.Api.Entities.Common.Primatives;

namespace TeachPlanner.Api.Entities.YearDataRecords;
public record YearDataCreatedDomainEvent(Guid Id, YearDataId YearDataId) : DomainEvent(Id);
