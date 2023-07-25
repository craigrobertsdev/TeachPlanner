using Domain.Common.Primatives;
using Domain.SubjectAggregates;
using Domain.SubjectAggregates.Entities;
using Domain.YearPlannerAggregate.ValueObjects;

namespace Domain.YearPlannerAggregate.Entities;
public sealed class TermPlan : Entity<TermPlanId>
{
    private readonly Dictionary<Subject, List<Strand>> _subjects = new();
    public IReadOnlyDictionary<Subject, List<Strand>> Subjects => _subjects.AsReadOnly();
}
