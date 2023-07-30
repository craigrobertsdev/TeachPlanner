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
    private readonly List<ResourceIdForReference> _resourceIds = new();
    private readonly List<AssessmentIdForReference> _assessmentIds = new();
    private readonly List<LessonComment> _comments = new();
    public TeacherIdForReference TeacherId { get; private set; }
    public SubjectIdForReference SubjectId { get; private set; }
    public string PlanningNotes { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public IReadOnlyList<ResourceIdForReference> ResourceIds => _resourceIds.AsReadOnly();
    public IReadOnlyList<AssessmentIdForReference> AssessmentIds => _assessmentIds.AsReadOnly();
    public IReadOnlyList<LessonComment> Comments => _comments.AsReadOnly();

    private LessonPlan(
        LessonPlanId id,
        TeacherIdForReference teacherId,
        SubjectIdForReference subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<ResourceIdForReference>? resourceIds,
        List<AssessmentIdForReference>? assessmentIds) : base(id)
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
        TeacherIdForReference teacherId,
        SubjectIdForReference subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        List<ResourceIdForReference>? resourceIds,
        List<AssessmentIdForReference>? assessmentIds)
    {
        return new LessonPlan(LessonPlanId.Create(), teacherId, subjectId, planningNotes, startTime, endTime, DateTime.UtcNow, DateTime.UtcNow, resourceIds, assessmentIds);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonPlan() { }
}
