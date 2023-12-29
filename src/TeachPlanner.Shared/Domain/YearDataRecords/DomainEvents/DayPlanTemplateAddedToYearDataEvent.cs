using TeachPlanner.Shared.Domain.Common.Primatives;
using TeachPlanner.Shared.Domain.PlannerTemplates;

namespace TeachPlanner.Shared.Domain.YearDataRecords.DomainEvents;

public record DayPlanTemplateAddedToYearDataEvent(Guid Id, DayPlanTemplateId DayPlanTemplateId) : DomainEvent(Id);
