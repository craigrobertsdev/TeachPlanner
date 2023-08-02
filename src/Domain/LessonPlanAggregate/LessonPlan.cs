using Domain.Assessments;
using Domain.Common.Primatives;
using Domain.ResourceAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;

namespace Domain.LessonPlanAggregate;

public sealed class LessonPlan : AggregateRoot
{
    private readonly List<Guid> _resourceIds = new();
    private readonly List<Guid> _summativeAssessmentIds = new();
    private readonly List<Guid> _formativeAssessmentIds = new();
    private readonly List<Guid> _comments = new();
    public TeacherId TeacherId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public string PlanningNotes { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public IReadOnlyList<Guid> ResourceIds => _resourceIds.AsReadOnly();
    public IReadOnlyList<Guid> SummativeAssessmentIds => _summativeAssessmentIds.AsReadOnly();
    public IReadOnlyList<Guid> FormativeAssessmentIds => _formativeAssessmentIds.AsReadOnly();
    public IReadOnlyList<Guid> Comments => _comments.AsReadOnly();

    private LessonPlan(
        Guid id,
        Guid teacherId,
        Guid subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<Guid>? resourceIds,
        List<Guid>? summativeAssessmentIds,
        List<Guid>? formativeAssessmentIds) : base(id)
    {
        TeacherId = teacherId;
        SubjectId = subjectId;
        PlanningNotes = planningNotes;
        StartTime = startTime;
        EndTime = endTime;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        if (resourceIds != null)
        {
            _resourceIds = resourceIds;
        }

        if (summativeAssessmentIds != null)
        {
            _summativeAssessmentIds = summativeAssessmentIds;
        }

        if (formativeAssessmentIds != null)
        {
            _formativeAssessmentIds = formativeAssessmentIds;
        }
    }

    public static LessonPlan Create(
        Guid teacherId,
        Guid subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        List<ResourceId>? resourceIds,
        List<SummativeAssessmentId>? summativeAssessmentIds,
        List<FormativeAssessmentId>? formativeAssessmentIds)
    {
        return new LessonPlan(Guid.NewGuid(), teacherId, subjectId, planningNotes, startTime, endTime, DateTime.UtcNow, DateTime.UtcNow, resourceIds, summativeAssessmentIds, formativeAssessmentIds);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonPlan() { }
}
