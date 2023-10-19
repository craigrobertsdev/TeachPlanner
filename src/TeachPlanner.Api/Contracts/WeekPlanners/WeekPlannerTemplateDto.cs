using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Contracts.WeekPlanners;

public record WeekPlannerTemplateDto(List<DayPlanDto> DayPlans)
{
    public static WeekPlannerTemplateDto Create(WeekPlannerTemplate template)
    {
        var dayPlans = template.DayPlanTemplates.Select(dayPlan => new DayPlanDto(
            dayPlan.Periods.Select(period => new PeriodDto(
                period.PeriodType.ToString(),
                period.StartTime,
                period.EndTime
            )).ToList()
        )).ToList();

        return new WeekPlannerTemplateDto(dayPlans);
    } 
}

public record DayPlanDto(List<PeriodDto> Periods);

public record PeriodDto(string PeriodType, DateTime StartTime, DateTime EndTime);