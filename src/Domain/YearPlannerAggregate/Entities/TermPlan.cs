using Domain.Common.Curriculum;
using Domain.Common.Curriculum.Entities;
using Domain.Common.Primatives;
using Domain.YearPlannerAggregate.ValueObjects;

namespace Domain.YearPlannerAggregate.Entities;
public class TermPlan : Entity<TermPlanId>
{
    private readonly Dictionary<Subject, List<Strand>> _subjects = new();
    public IReadOnlyDictionary<Subject, List<Strand>> Subjects => _subjects.AsReadOnly();
}
