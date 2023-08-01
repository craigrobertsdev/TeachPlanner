using Domain.Assessments;
using Domain.Common.Primatives;
using Domain.ResourceAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;

namespace Domain.LessonPlanAggregate;

public sealed class LessonPlan : AggregateRoot<LessonPlanId>
{
    private readonly List<ResourceId> _resourceIds = new();
    private readonly List<SummativeAssessmentId> _summativeAssessmentIds = new();
    private readonly List<FormativeAssessmentId> _formativeAssessmentIds = new();
    private readonly List<LessonComment> _comments = new();
    public TeacherId TeacherId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public string PlanningNotes { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public IReadOnlyList<ResourceId> ResourceIds => _resourceIds.AsReadOnly();
    public IReadOnlyList<SummativeAssessmentId> SummativeAssessmentIds => _summativeAssessmentIds.AsReadOnly();
    public IReadOnlyList<FormativeAssessmentId> FormativeAssessmentIds => _formativeAssessmentIds.AsReadOnly();
    public IReadOnlyList<LessonComment> Comments => _comments.AsReadOnly();

    private LessonPlan(
        LessonPlanId id,
        TeacherId teacherId,
        SubjectId subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<ResourceId>? resourceIds,
        List<SummativeAssessmentId>? summativeAssessmentIds,
        List<FormativeAssessmentId>? formativeAssessmentIds) : base(id)
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
        TeacherId teacherId,
        SubjectId subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        List<ResourceId>? resourceIds,
        List<SummativeAssessmentId>? summativeAssessmentIds,
        List<FormativeAssessmentId>? formativeAssessmentIds)
    {
        return new LessonPlan(new LessonPlanId(Guid.NewGuid()), teacherId, subjectId, planningNotes, startTime, endTime, DateTime.UtcNow, DateTime.UtcNow, resourceIds, summativeAssessmentIds, formativeAssessmentIds);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonPlan() { }
}
