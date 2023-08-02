using Domain.Common.Primatives;

namespace Domain.TeacherAggregate;

public sealed class Teacher : AggregateRoot
{
    private readonly List<Guid> _subjectIds = new();
    private readonly List<Guid> _studentIds = new();
    private readonly List<Guid> _summativeAssessmentIds = new();
    private readonly List<Guid> _formativeAssessmentIds = new();
    private readonly List<Guid> _resourceIds = new();
    private readonly List<Guid> _reportIds = new();
    private readonly List<Guid> _lessonPlanIds = new();
    private readonly Guid _userId;
    public Guid UserId => _userId;
    public IReadOnlyList<Guid> SubjectIds => _subjectIds;
    public IReadOnlyList<Guid> StudentIds => _studentIds;
    public IReadOnlyList<Guid> SummativeAssessmentIds => _summativeAssessmentIds;
    public IReadOnlyList<Guid> FormativeAssessmentIds => _formativeAssessmentIds;
    public IReadOnlyList<Guid> ResourceIds => _resourceIds;
    public IReadOnlyList<Guid> ReportIds => _reportIds;
    public IReadOnlyList<Guid> LessonPlanIds => _lessonPlanIds;

    private Teacher(
        Guid id,
        Guid userId) : base(id)
    {
        _userId = userId;
    }

    public static Teacher Create(Guid userId)
    {
        return new(Guid.NewGuid(), userId);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Teacher() { }
}
