using TeachPlanner.Domain.Common.Primatives;
using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Domain.TermPlanner;
public sealed class TermPlan : Entity
{
    private readonly List<Subject> _subjects = new();
    public IReadOnlyList<Subject> Subjects => _subjects.AsReadOnly();

    private TermPlan(Guid id, List<Subject> subjects) : base(id)
    {
        _subjects = subjects;
    }

    public void AddSubjects(List<Subject> subjects)
    {
        foreach (var subject in subjects)
        {
            if (_subjects.Find(s => s.Id == subject.Id) == null)
            {
                _subjects.Add(subject);
            }
        }
    }

    public static TermPlan Create(List<Subject> subjects)
    {
        return new TermPlan(Guid.NewGuid(), subjects);
    }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private TermPlan() { }
}
