using Domain.Common.Primatives;

namespace Domain.TeacherAggregate;

public sealed class Teacher : AggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    private readonly List<Guid> _subjectIds = new();
    private readonly List<Guid> _studentIds = new();
    private readonly List<Guid> _summativeAssessmentIds = new();
    private readonly List<Guid> _formativeAssessmentIds = new();
    private readonly List<Guid> _resourceIds = new();
    private readonly List<Guid> _reportIds = new();
    private readonly List<Guid> _lessonPlanIds = new();
    public IReadOnlyList<Guid> SubjectIds => _subjectIds;
    public IReadOnlyList<Guid> StudentIds => _studentIds;
    public IReadOnlyList<Guid> SummativeAssessmentIds => _summativeAssessmentIds;
    public IReadOnlyList<Guid> FormativeAssessmentIds => _formativeAssessmentIds;
    public IReadOnlyList<Guid> ResourceIds => _resourceIds;
    public IReadOnlyList<Guid> ReportIds => _reportIds;
    public IReadOnlyList<Guid> LessonPlanIds => _lessonPlanIds;

    private Teacher(Guid id, string firstName, string lastName, string email, string password) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public static Teacher Create(string firstName, string lastName, string email, string password)
    {
        return new(Guid.NewGuid(), firstName, lastName, email, password);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Teacher() { }
}
