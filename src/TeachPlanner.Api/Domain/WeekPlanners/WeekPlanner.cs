using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Domain.PlannerTemplates;

public sealed class WeekPlanner : Entity<WeekPlannerId>, IAggregateRoot
{
    private readonly List<DayPlan> _dayPlans = new();
    public YearDataId YearDataId { get; private set; }
    public WeekPlannerTemplate WeekPlanPattern { get; private set; }
    public DateTime WeekStart { get; private set; }
    public int WeekNumber { get; private set; }
    public int TermNumber { get; private set; }
    public int Year { get; private set; }
    public IReadOnlyList<DayPlan> DayPlans => _dayPlans.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }
    
    public void AddDayPlan(DayPlan dayPlan)
    {
        if (_dayPlans.Count >= 5)
        {
            throw new TooManyDayPlansInWeekPlannerException();
        }
        _dayPlans.Add(dayPlan);
    }

    private WeekPlanner(
        WeekPlannerId id,
        YearDataId yearDataId,
        int weekNumber,
        int termNumber,
        int year,
        WeekPlannerTemplate weekPlanPattern,
        DateTime weekStart) : base(id)
    {
        YearDataId = yearDataId;
        WeekStart = weekStart;
        WeekNumber = weekNumber;
        TermNumber = termNumber;
        Year = year;
        WeekPlanPattern = weekPlanPattern;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }
    public static WeekPlanner Create(
        YearDataId yearDataId,
        int weekNumber,
        int termNumber,
        int year,
        WeekPlannerTemplate weekPlanPattern,
        DateTime weekStart)
    {
        return new WeekPlanner(
            new WeekPlannerId(Guid.NewGuid()),
            yearDataId,
            weekNumber,
            termNumber,
            year,
            weekPlanPattern,
            weekStart);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WeekPlanner()
    {
    }
}