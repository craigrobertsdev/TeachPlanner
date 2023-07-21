using Domain.Common.Assessment.ValueObjects;
using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Primatives;
using Domain.LessonAggregate.Entities;
using Domain.LessonAggregate.ValueObjects;
using Domain.Resource.ValueObjects;

namespace Domain.LessonAggregate;

public sealed class Lesson : AggregateRoot<LessonId>
{
    private readonly List<ResourceId> _resourceIds = new();
    private readonly List<AssessmentId> _assessmentIds = new();
    private readonly List<Comment> _comments = new();
    public SubjectId SubjectId { get; private set; }
    public string PlanningNotes { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public IReadOnlyList<ResourceId> Resources => _resourceIds.AsReadOnly();
    public IReadOnlyList<AssessmentId> Assessments => _assessmentIds.AsReadOnly();
    public IReadOnlyList<Comment> Comments => _comments.AsReadOnly();

    private Lesson(
        SubjectId subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        DateTime createdDateTime,
        DateTime updatedDateTime)
    {
        SubjectId = subjectId;
        PlanningNotes = planningNotes;
        StartTime = startTime;
        EndTime = endTime;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Lesson Create(
        SubjectId subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime)
    {
        return new Lesson(subjectId, planningNotes, startTime, endTime, DateTime.UtcNow, DateTime.UtcNow);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Lesson() { }
}
