using Domain.Common.Primatives;
using Domain.SubjectAggregates;

namespace Domain.YearPlannerAggregate;
public sealed class TermPlan : ValueObject
{
    private readonly Dictionary<SubjectId, List<Strand>> _subjects = new();
    public IReadOnlyDictionary<SubjectId, List<Strand>> Subjects => _subjects.AsReadOnly();

    private TermPlan(
        Dictionary<SubjectId, List<Strand>> subjects)
    {
        _subjects = subjects;
    }

    public static TermPlan Create(Dictionary<SubjectId, List<Strand>> subjects)
    {
        return new TermPlan(subjects);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Subjects;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlan() { }
}
