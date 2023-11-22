using TeachPlanner.Api.Domain.Common.Primatives;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public class DayPlanTemplate: Entity<DayPlanTemplateId>
{
    private readonly List<TemplatePeriod> _periods = new();

    public IReadOnlyList<TemplatePeriod> Periods => _periods.AsReadOnly();
    public int NumberOfPeriods => Periods.Count;

    private DayPlanTemplate(DayPlanTemplateId id, List<TemplatePeriod> periods) : base(id)
    {
        _periods = periods;
    }
    
    public static DayPlanTemplate Create(List<TemplatePeriod> periods)
    {
        return new DayPlanTemplate(new DayPlanTemplateId(Guid.NewGuid()), periods);
    }
    
    #pragma warning disable CS8618
    private DayPlanTemplate() { }
}