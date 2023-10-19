using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Domain.PlannerTemplatesPe;

public record DayPlanTemplate
{
    private readonly List<Period> _periods = new();

    public IReadOnlyList<Period> Periods => _periods.AsReadOnly();

    private DayPlanTemplate(List<Period> periods)
    {
        _periods = periods;
    }
    
    public static DayPlanTemplate Create(List<Period> periods)
    {
        return new DayPlanTemplate(periods);
    }
    
    #pragma warning disable CS8618
    private DayPlanTemplate() { }
}