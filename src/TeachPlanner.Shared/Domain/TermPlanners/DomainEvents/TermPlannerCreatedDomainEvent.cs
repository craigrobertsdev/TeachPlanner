using TeachPlanner.Shared.Domain.Common.Primatives;
using TeachPlanner.Shared.Domain.TermPlanners;
using TeachPlanner.Shared.Domain.YearDataRecords;

namespace TeachPlanner.Shared.Domain.TermPlanners.DomainEvents;

public record TermPlannerCreatedDomainEvent
    (Guid Id, TermPlannerId TermPlannerId, YearDataId YearDataId) : DomainEvent(Id);