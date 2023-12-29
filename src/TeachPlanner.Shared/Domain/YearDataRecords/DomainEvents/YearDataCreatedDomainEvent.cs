using TeachPlanner.Shared.Domain.Common.Primatives;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Shared.Domain.YearDataRecords.DomainEvents;

public record YearDataCreatedDomainEvent
    (Guid Id, YearDataId YearDataId, int CalendarYear, TeacherId TeacherId) : DomainEvent(Id);