using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.PlannerTemplatesPe;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public class WeekPlannerTemplate : Entity<WeekPlannerTemplateId>
{
    private readonly List<DayPlanTemplate> _dayPlanTemplates = new();

    private WeekPlannerTemplate(
        WeekPlannerTemplateId id,
        List<DayPlanTemplate> dayPlanTemplates) : base(id)
    {
        _dayPlanTemplates = dayPlanTemplates;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public IReadOnlyList<DayPlanTemplate> DayPlanTemplates => _dayPlanTemplates.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public static WeekPlannerTemplate Create(List<DayPlanTemplate> dayPlanTemplates)
    {
        return new WeekPlannerTemplate(
            new WeekPlannerTemplateId(Guid.NewGuid()),
            dayPlanTemplates);
    }
    
    #pragma warning disable CS8618
    private WeekPlannerTemplate() { }
}