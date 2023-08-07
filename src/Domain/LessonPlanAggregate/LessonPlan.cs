using Domain.Common.Primatives;
using Domain.ResourceAggregate;

namespace Domain.LessonPlanAggregate;

public sealed class LessonPlan : AggregateRoot
{
    private readonly List<Resource> _resources = new();
    private readonly List<Guid> _summativeAssessmentIds = new();
    private readonly List<Guid> _formativeAssessmentIds = new();
    private readonly List<LessonComment> _comments = new();
    public Guid TeacherId { get; private set; }
    public Guid SubjectId { get; private set; }
    public string PlanningNotes { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public IReadOnlyList<Resource> Resources => _resources.AsReadOnly();
    public IReadOnlyList<Guid> SummativeAssessmentIds => _summativeAssessmentIds.AsReadOnly();
    public IReadOnlyList<Guid> FormativeAssessmentIds => _formativeAssessmentIds.AsReadOnly();
    public IReadOnlyList<LessonComment> Comments => _comments.AsReadOnly();

    private LessonPlan(
        Guid id,
        Guid teacherId,
        Guid subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<Resource>? resources,
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

        if (resources != null)
        {
            _resources = resources;
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
        List<Resource>? resources,
        List<Guid>? summativeAssessmentIds,
        List<Guid>? formativeAssessmentIds)
    {
        return new LessonPlan(Guid.NewGuid(), teacherId, subjectId, planningNotes, startTime, endTime, DateTime.UtcNow, DateTime.UtcNow, resources, summativeAssessmentIds, formativeAssessmentIds);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonPlan() { }
}
