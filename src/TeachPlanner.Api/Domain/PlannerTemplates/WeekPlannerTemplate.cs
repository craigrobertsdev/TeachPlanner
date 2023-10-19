using TeachPlanner.Api.Domain.Common.Primatives;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public class WeekPlannerTemplate : Entity<WeekPlannerTemplateId>
{
    public DayPlanTemplate DayPlanTemplate { get; private set; }

    private WeekPlannerTemplate(
        WeekPlannerTemplateId id,
        DayPlanTemplate dayPlanTemplate) : base(id)
    {
        DayPlanTemplate = dayPlanTemplate;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public static WeekPlannerTemplate Create(DayPlanTemplate dayPlanTemplate)
    {
        return new WeekPlannerTemplate(
            new WeekPlannerTemplateId(Guid.NewGuid()),
            dayPlanTemplate);
    }
    
    #pragma warning disable CS8618
    private WeekPlannerTemplate() { }
}