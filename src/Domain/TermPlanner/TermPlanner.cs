using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.TermPlanner;
public sealed class TermPlanner : AggregateRoot
{
    private readonly List<TermPlan> _termPlans = new();
    public YearLevelValue YearLevel { get; private set; }
    public IReadOnlyList<TermPlan> TermPlans => _termPlans.AsReadOnly();

    private TermPlanner(Guid id, YearLevelValue yearLevel, List<TermPlan> termPlans) : base(id)
    {
        YearLevel = yearLevel;
        _termPlans = termPlans;
    }

    public static TermPlanner Create(YearLevelValue yearLevel, List<TermPlan> termPlans)
    {
        return new TermPlanner(
            Guid.NewGuid(),
            yearLevel,
            termPlans);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlanner() { }
}
