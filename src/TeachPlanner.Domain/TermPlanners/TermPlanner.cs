using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Exceptions;
using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.TermPlanners;
public sealed class TermPlanner : AggregateRoot
{
    private readonly List<TermPlan> _termPlans = new();
    private readonly List<YearLevelValue> _yearLevels = new();
    public IReadOnlyList<TermPlan> TermPlans => _termPlans.AsReadOnly();
    public IReadOnlyList<YearLevelValue> YearLevels => _yearLevels.AsReadOnly();
    public int CalendarYear { get; private set; }
    public Guid TeacherId { get; private set; }

    private TermPlanner(Guid id, Guid teacherId, int calendarYear, YearLevelValue firstYearLevel, YearLevelValue? secondYearLevel) : base(id)
    {
        TeacherId = teacherId;
        CalendarYear = calendarYear;
        _yearLevels.Add(firstYearLevel);

        if (secondYearLevel != null)
        {
            _yearLevels.Add((YearLevelValue)secondYearLevel);
        }

        SortYearLevels();
    }

    public static TermPlanner Create(Guid teacherId, int calendarYear, YearLevelValue firstYearLevel, YearLevelValue? secondYearLevel)
    {

        if (firstYearLevel == secondYearLevel)
        {
            secondYearLevel = null;
        }

        return new TermPlanner(
            Guid.NewGuid(),
            teacherId,
            calendarYear,
            firstYearLevel,
            secondYearLevel);
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
        if (CanAddMoreYearLevels())
        {
            return; // cannot sort just 1 year level
        }

        if ((int)_yearLevels[0]! > (int)_yearLevels[1]!)
        {
            (_yearLevels[1], _yearLevels[0]) = (_yearLevels[0], _yearLevels[1]);
        }
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