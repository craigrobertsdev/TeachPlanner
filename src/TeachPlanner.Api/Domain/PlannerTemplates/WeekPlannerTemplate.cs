using TeachPlanner.Api.Domain.Common.Primatives;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public class WeekPlannerTemplate : Entity<WeekPlannerTemplateId>
{
    private readonly List<DayPlanTemplate> _dayPlans = new();

    private WeekPlannerTemplate(
        WeekPlannerTemplateId id,
        List<DayPlanTemplate> dayPlans) : base(id)
    {
        _dayPlans = dayPlans;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public IReadOnlyList<DayPlanTemplate> DayPlans => _dayPlans.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public static WeekPlannerTemplate Create(List<DayPlanTemplate> dayPlans)
    {
        return new WeekPlannerTemplate(
            new WeekPlannerTemplateId(Guid.NewGuid()),
            dayPlans);
    }
}