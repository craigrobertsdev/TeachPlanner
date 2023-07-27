using Domain.Assessments.ValueObjects;
using Domain.Common.Primatives;
using Domain.LessonPlanAggregate.Entities;
using Domain.LessonPlanAggregate.ValueObjects;
using Domain.ResourceAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;

namespace Domain.LessonPlanAggregate;

public sealed class LessonPlan : AggregateRoot<LessonPlanId>
{
    private readonly List<ResourceId> _resourceIds = new();
    private readonly List<AssessmentId> _assessmentIds = new();
    private readonly List<LessonComment> _comments = new();
    public TeacherId TeacherId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public string PlanningNotes { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public IReadOnlyList<ResourceId> ResourceIds => _resourceIds.AsReadOnly();
    public IReadOnlyList<AssessmentId> AssessmentIds => _assessmentIds.AsReadOnly();
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
        List<AssessmentId>? assessmentIds) : base(id)
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

        if (assessmentIds != null)
        {
            _assessmentIds = assessmentIds;
        }
    }

    public static LessonPlan Create(
        TeacherId teacherId,
        SubjectId subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        List<ResourceId>? resourceIds,
        List<AssessmentId>? assessmentIds)
    {
        return new LessonPlan(LessonPlanId.Create(), teacherId, subjectId, planningNotes, startTime, endTime, DateTime.UtcNow, DateTime.UtcNow, resourceIds, assessmentIds);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonPlan() { }
}
