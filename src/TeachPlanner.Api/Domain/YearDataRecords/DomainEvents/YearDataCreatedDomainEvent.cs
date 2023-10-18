using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Domain.YearDataRecords.DomainEvents;

public record YearDataCreatedDomainEvent
    (Guid Id, YearDataId YearDataId, int CalendarYear, TeacherId TeacherId) : DomainEvent(Id);