namespace TeachPlanner.Api.Domain.PlannerTemplates;

public record DayPlanTemplate
{
    private readonly List<PeriodType> _periods = new();
    public IReadOnlyList<PeriodType> Periods => _periods.AsReadOnly();
    public DayPlanTemplate(List<PeriodType> periods)
    {
        _periods = periods;
    }

    public static DayPlanTemplate Create(List<PeriodType> periods)
    {
        return new DayPlanTemplate(periods);
    }
}