using Domain.Assessments.ValueObjects;
using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Primatives;
using Domain.ReportAggregate.ValueObjects;
using Domain.StudentAggregate.ValueObjects;

namespace Domain.StudentAggregate;

public class Student : AggregateRoot<StudentId>
{
    private readonly List<ReportId> _reportIds = new();
    private readonly List<SubjectId> _subjectIds = new();
    private readonly List<AssessmentId> _assessmentIds = new();

    public IReadOnlyList<ReportId> ReportIds => _reportIds;
    public IReadOnlyList<SubjectId> SubjectIds => _subjectIds;
    public IReadOnlyList<AssessmentId> AssessmentIds => _assessmentIds;

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
