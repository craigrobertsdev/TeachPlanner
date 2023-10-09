using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Domain.TermPlanners.DomainEvents;

public record TermPlannerCreatedDomainEvent(Guid Id, TermPlannerId TermPlannerId, YearDataId YearDataId) : DomainEvent(Id);
