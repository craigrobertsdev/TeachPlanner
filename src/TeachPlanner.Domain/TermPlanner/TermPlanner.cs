using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Primatives;
using TeachPlanner.Domain.Common.Exceptions;

namespace TeachPlanner.Domain.TermPlanner;
public sealed class TermPlanner : AggregateRoot
{
    private readonly List<TermPlan> _termPlans = new();
    public int CalendarYear { get; private set; }
    public YearLevelValue? YearLevel { get; set; }
    private readonly List<YearLevelValue> _yearLevels = new();
    public IReadOnlyList<TermPlan> TermPlans => _termPlans.AsReadOnly();
    public IReadOnlyList<YearLevelValue> YearLevels => _yearLevels.AsReadOnly();

    private TermPlanner(Guid id, int calendarYear, List<TermPlan> termPlans, List<YearLevelValue> yearLevels, YearLevelValue? yearLevel = null) : base(id)
    {
        CalendarYear = calendarYear;
        _termPlans = termPlans;
        YearLevel = yearLevel;
        _yearLevels = yearLevels;
    }

    public static TermPlanner Create(int calendarYear, List<TermPlan> termPlans, YearLevelValue? yearLevel = null, List<YearLevelValue>? yearLevels = null)
    {
        if (yearLevel is null && yearLevels is null)
        {
            throw new TermPlannerCreationException();
        }

        return new TermPlanner(
            Guid.NewGuid(),
            calendarYear,
            termPlans,
            yearLevels ?? new List<YearLevelValue>(),
            yearLevel);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlanner() { }
}
