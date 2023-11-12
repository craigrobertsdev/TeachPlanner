using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Contracts.WeekPlanners;

public record WeekPlannerTemplateDto(DayPlanDto DayPlanTemplate)
{
    public static WeekPlannerTemplateDto Create(DayPlanTemplate template)
    {
        var dayPlan = new DayPlanDto(
            template.Periods.Select(period => new PeriodDto(
                period.PeriodType.ToString(),
                period.StartTime,
                period.EndTime
            )).ToList());

        return new WeekPlannerTemplateDto(dayPlan);
    } 
}

public record DayPlanDto(List<PeriodDto> Periods);

public record PeriodDto(string PeriodType, TimeOnly StartTime, TimeOnly EndTime);