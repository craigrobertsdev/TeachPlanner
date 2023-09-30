using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Entities.Common.Enums;
using TeachPlanner.Api.Entities.Common.Interfaces;
using TeachPlanner.Api.Entities.Common.Primatives;
using TeachPlanner.Api.Entities.YearDataRecords;

namespace TeachPlanner.Api.Entities.TermPlanners;
public sealed class TermPlanner : Entity<TermPlannerId>, IAggregateRoot
{
    private readonly List<TermPlan> _termPlans = new();
    private readonly List<YearLevelValue> _yearLevels = new();
    public IReadOnlyList<TermPlan> TermPlans => _termPlans.AsReadOnly();
    public IReadOnlyList<YearLevelValue> YearLevels => _yearLevels.AsReadOnly();
    public int CalendarYear { get; private set; }
    public YearDataId YearDataId { get; private set; }

    private TermPlanner(TermPlannerId id, YearDataId yearDataId, int calendarYear, List<YearLevelValue> yearLevels) : base(id)
    {
        YearDataId = yearDataId;
        CalendarYear = calendarYear;
        _yearLevels = yearLevels;

        SortYearLevels();
    }

    public static TermPlanner Create(YearDataId yearDataId, int calendarYear, List<YearLevelValue> yearLevels)
    {

        yearLevels = RemoveDuplicateYearLevels(yearLevels);

        return new TermPlanner(
            new TermPlannerId(Guid.NewGuid()),
            yearDataId,
            calendarYear,
            yearLevels);
    }

    private static List<YearLevelValue> RemoveDuplicateYearLevels(List<YearLevelValue> yearLevels)
    {
        return yearLevels.Distinct().ToList();
    }

    public void AddYearLevel(YearLevelValue yearLevel)
    {
        if (CanAddNoMoreYearLevels())
        {
            throw new TooManyYearLevelsException();
        }

        if (_yearLevels.Contains(yearLevel))
        {
            throw new InputException("Year level already exists");
        }

        _yearLevels.Add(yearLevel);
        SortYearLevels();
    }

    public bool CanAddNoMoreYearLevels()
    {
        return _yearLevels.Count >= 2;
    }

    public bool CanAddMoreYearLevels()
    {
        return !CanAddNoMoreYearLevels();
    }

    public void SortYearLevels()
    {
        if (_yearLevels.Count == 1)
        {
            return;
        }

        _yearLevels.Sort();
    }

    public void AddTermPlan(TermPlan termPlan)
    {
        if (_termPlans.Contains(termPlan))
        {
            throw new DuplicateTermPlanException();
        }

        if (_termPlans.Count >= 4)
        {
            throw new TooManyTermPlansException();
        }

        if (_termPlans.Any(tp => tp.TermNumber == termPlan.TermNumber))
        {
            throw new DuplicateTermNumberException();
        }

        _termPlans.Add(termPlan);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlanner() { }
}