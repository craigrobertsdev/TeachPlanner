using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.TermPlanners;
public sealed class TermPlan : Entity
{
    private readonly List<string> _curriculumCodes = new();
    public IReadOnlyList<string> CurriulumCodes => _curriculumCodes.AsReadOnly();

    private TermPlan(Guid id, List<string> curriculumCodes) : base(id)
    {
        _curriculumCodes = curriculumCodes;
    }

    public void AddCurriculumCode(string curriculumCode)
    {
        if (!_curriculumCodes.Contains(curriculumCode))
        {
            _curriculumCodes.Add(curriculumCode);
        }
    }

    public static TermPlan Create(List<string> curriculumCodes)
    {
        return new TermPlan(Guid.NewGuid(), curriculumCodes);
    }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlan() { }
}
