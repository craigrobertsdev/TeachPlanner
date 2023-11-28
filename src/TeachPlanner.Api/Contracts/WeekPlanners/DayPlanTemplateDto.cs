using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Contracts.WeekPlanners;

public record DayPlanTemplateDto(List<PeriodDto> Pattern) {
    public static DayPlanTemplateDto Create(DayPlanTemplate dayPlanTemplate) {
        var dayPlan = new List<PeriodDto>(dayPlanTemplate.Periods.Select(
            period => new PeriodDto(
                period.PeriodType.ToString(),
                period.Name ?? null,
                period.StartTime,
                period.EndTime)).ToList()); 

        return new DayPlanTemplateDto(dayPlan);
    }
}

public record PeriodDto(string PeriodType, string? Name, TimeOnly StartTime, TimeOnly EndTime);