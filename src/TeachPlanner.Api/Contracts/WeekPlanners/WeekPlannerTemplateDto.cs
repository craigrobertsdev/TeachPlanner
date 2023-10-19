using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Contracts.WeekPlanners;

public record WeekPlannerTemplateDto(DayPlanDto DayPlanTemplate)
{
    public static WeekPlannerTemplateDto Create(WeekPlannerTemplate template)
    {
        var dayPlan = new DayPlanDto(
            template.DayPlanTemplate.Periods.Select(period => new PeriodDto(
                period.PeriodType.ToString(),
                period.StartTime,
                period.EndTime
            )).ToList());

        return new WeekPlannerTemplateDto(dayPlan);
    } 
}

public record DayPlanDto(List<PeriodDto> Periods);

public record PeriodDto(string PeriodType, DateTime StartTime, DateTime EndTime);