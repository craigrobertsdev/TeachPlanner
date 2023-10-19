using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public record DayPlanTemplate
{
    private readonly List<TemplatePeriod> _periods = new();

    public IReadOnlyList<TemplatePeriod> Periods => _periods.AsReadOnly();

    private DayPlanTemplate(List<TemplatePeriod> periods)
    {
        _periods = periods;
    }
    
    public static DayPlanTemplate Create(List<TemplatePeriod> periods)
    {
        return new DayPlanTemplate(periods);
    }
    
    #pragma warning disable CS8618
    private DayPlanTemplate() { }
}