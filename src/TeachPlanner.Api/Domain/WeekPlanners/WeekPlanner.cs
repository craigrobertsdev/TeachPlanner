using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public sealed class WeekPlanner : Entity<WeekPlannerId>, IAggregateRoot
{
    private readonly List<DayPlan> _dayPlans = new();

    private WeekPlanner(
        WeekPlannerId id,
        YearDataId yearDataId,
        WeekPlannerTemplate weekPlanPattern,
        DateTime weekStart) : base(id)
    {
        YearDataId = yearDataId;
        WeekStart = weekStart;
        WeekPlanPattern = weekPlanPattern;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public YearDataId YearDataId { get; private set; }
    public WeekPlannerTemplate WeekPlanPattern { get; private set; }
    public DateTime WeekStart { get; private set; }
    public int WeekNumber { get; }
    public IReadOnlyList<DayPlan> DayPlans => _dayPlans.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public static WeekPlanner Create(
        YearDataId yearDataId,
        WeekPlannerTemplate weekPlanPattern,
        DateTime weekStart)
    {
        return new WeekPlanner(
            new WeekPlannerId(Guid.NewGuid()),
            yearDataId,
            weekPlanPattern,
            weekStart);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WeekPlanner()
    {
    }
}