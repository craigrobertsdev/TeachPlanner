using Domain.Assessments;
using Domain.Common.Primatives;
using Domain.ReportAggregate;
using Domain.TeacherAggregate;

namespace Domain.StudentAggregate;

public sealed class Student : AggregateRoot<StudentId>
{
    private readonly List<ReportId> _reportIds = new();
    private readonly List<SummativeAssessmentId> _summativeAssessmentIds = new();
    private readonly List<FormativeAssessmentId> _formativeAssessmentIds = new();
    public TeacherId? TeacherId { get; private set; }

    public IReadOnlyList<ReportId> ReportIds => _reportIds;
    public IReadOnlyList<SummativeAssessmentId> SummativeAssessmentIds => _summativeAssessmentIds;
    public IReadOnlyList<FormativeAssessmentId> FormativeAssessmentIds => _formativeAssessmentIds;

    private Student(StudentId id) : base(id)
    {
    }

    public static Student Create()
    {
        return new Student(new StudentId(Guid.NewGuid()));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Student() { }
}
