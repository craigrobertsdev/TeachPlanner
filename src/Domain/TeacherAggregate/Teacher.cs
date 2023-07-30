using Domain.Assessments.ValueObjects;
using Domain.Common.Primatives;
using Domain.ReportAggregate.ValueObjects;
using Domain.ResourceAggregate.ValueObjects;
using Domain.StudentAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
using Domain.UserAggregate.ValueObjects;

namespace Domain.TeacherAggregate;

public sealed class Teacher : AggregateRoot<TeacherId>
{
    private readonly List<SubjectIdForReference> _subjectIds = new();
    private readonly List<StudentIdForReference> _studentIds = new();
    private readonly List<AssessmentIdForReference> _assessmentIds = new();
    private readonly List<ResourceIdForReference> _resourceIds = new();
    private readonly List<ReportIdForReference> _reportIds = new();
    private readonly UserIdForReference _userId;
    public UserIdForReference UserId => _userId;
    public IReadOnlyList<SubjectIdForReference> SubjectIds => _subjectIds;
    public IReadOnlyList<StudentIdForReference> StudentIds => _studentIds;
    public IReadOnlyList<AssessmentIdForReference> AssessmentIds => _assessmentIds;
    public IReadOnlyList<ResourceIdForReference> ResourceIds => _resourceIds;
    public IReadOnlyList<ReportIdForReference> ReportIds => _reportIds;

    private Teacher(
        TeacherId id,
        UserIdForReference userId) : base(id)
    {
        _userId = userId;
    }

    public static Teacher Create(UserIdForReference userId)
    {
        return new Teacher(TeacherId.Create(), userId);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Teacher() { }
}
