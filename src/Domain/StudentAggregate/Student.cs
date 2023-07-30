using Domain.Assessments.ValueObjects;
using Domain.Common.Primatives;
using Domain.ReportAggregate.ValueObjects;
using Domain.StudentAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;

namespace Domain.StudentAggregate;

public sealed class Student : AggregateRoot<StudentId>
{
    private readonly List<ReportIdForReference> _reportIds = new();
    private readonly List<SubjectIdForReference> _subjectIds = new();
    private readonly List<AssessmentIdForReference> _assessmentIds = new();

    public IReadOnlyList<ReportIdForReference> ReportIds => _reportIds;
    public IReadOnlyList<SubjectIdForReference> SubjectIds => _subjectIds;
    public IReadOnlyList<AssessmentIdForReference> AssessmentIds => _assessmentIds;

    private Student(StudentId id) : base(id)
    {
    }

    public static Student Create()
    {
        return new Student(StudentId.Create());
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Student() { }
}
