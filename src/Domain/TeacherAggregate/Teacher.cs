using Domain.Assessments;
using Domain.Common.Primatives;
using Domain.LessonPlanAggregate;
using Domain.ReportAggregate;
using Domain.ResourceAggregate;
using Domain.StudentAggregate;
using Domain.SubjectAggregates;
using Domain.UserAggregate;

namespace Domain.TeacherAggregate;

public sealed class Teacher : AggregateRoot<TeacherId>
{
    private readonly List<SubjectId> _subjectIds = new();
    private readonly List<StudentId> _studentIds = new();
    private readonly List<SummativeAssessmentId> _summativeAssessmentIds = new();
    private readonly List<FormativeAssessmentId> _formativeAssessmentIds = new();
    private readonly List<ResourceId> _resourceIds = new();
    private readonly List<ReportId> _reportIds = new();
    private readonly List<LessonPlan> _lessonPlans = new();
    private readonly UserId _userId;
    public UserId UserId => _userId;
    public IReadOnlyList<SubjectId> SubjectIds => _subjectIds;
    public IReadOnlyList<StudentId> StudentIds => _studentIds;
    public IReadOnlyList<SummativeAssessmentId> SummativeAssessmentIds => _summativeAssessmentIds;
    public IReadOnlyList<FormativeAssessmentId> FormativeAssessmentIds => _formativeAssessmentIds;
    public IReadOnlyList<ResourceId> ResourceIds => _resourceIds;
    public IReadOnlyList<ReportId> ReportIds => _reportIds;
    public IReadOnlyList<LessonPlan> LessonPlans => _lessonPlans;

    private Teacher(
        TeacherId id,
        UserId userId) : base(id)
    {
        _userId = userId;
    }

    public static Teacher Create(UserId userId)
    {
        return new(new TeacherId(Guid.NewGuid()), userId);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Teacher() { }
}
