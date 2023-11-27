using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Domain.YearDataRecords.DomainEvents;

public record DayPlanTemplateAddedToYearDataEvent(Guid Id, DayPlanTemplateId DayPlanTemplateId) : DomainEvent(Id);
