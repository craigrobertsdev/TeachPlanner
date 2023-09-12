using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Primatives;
using OneOf;

namespace TeachPlanner.Domain.TermPlanner;
public sealed class TermPlanner : AggregateRoot
{
    private readonly List<TermPlan> _termPlans = new();
    public int CalendarYear { get; private set; }
    public OneOf<YearLevelValue, List<YearLevelValue>> YearLevels { get; private set; }
    public IReadOnlyList<TermPlan> TermPlans => _termPlans.AsReadOnly();

    private TermPlanner(Guid id, int calendarYear, OneOf<YearLevelValue, List<YearLevelValue>> yearLevels, List<TermPlan> termPlans) : base(id)
    {
        CalendarYear = calendarYear;
        YearLevels = yearLevels;
        _termPlans = termPlans;
    }

    public static TermPlanner Create(int calendarYear, OneOf<YearLevelValue, List<YearLevelValue>> yearLevels, List<TermPlan> termPlans)
    {
        return new TermPlanner(
            Guid.NewGuid(),
            calendarYear,
            yearLevels,
            termPlans);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlanner() { }
}
