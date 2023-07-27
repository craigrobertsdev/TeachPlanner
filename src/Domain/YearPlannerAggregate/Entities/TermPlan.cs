using Domain.Common.Primatives;
using Domain.SubjectAggregates.ValueObjects;
using Domain.YearPlannerAggregate.ValueObjects;

namespace Domain.YearPlannerAggregate.Entities;
public sealed class TermPlan : Entity<TermPlanId>
{
    private readonly Dictionary<SubjectId, List<StrandId>> _subjects = new();
    public IReadOnlyDictionary<SubjectId, List<StrandId>> Subjects => _subjects.AsReadOnly();

    private TermPlan(
        TermPlanId id,
        Dictionary<SubjectId, List<StrandId>> subjects) : base(id)
    {
        _subjects = subjects;
    }

    public static TermPlan Create(Dictionary<SubjectId, List<StrandId>> subjects)
    {
        return new TermPlan(
            new TermPlanId(Guid.NewGuid()),
            subjects);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlan() { }
}
