using TeachPlanner.Domain.Common.Primatives;
using TeachPlanner.Domain.Resources;

namespace TeachPlanner.Domain.LessonPlans;

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
    public int NumberOfPeriods { get; private set; }

    public IReadOnlyList<Resource> Resources => _resources.AsReadOnly();
    public IReadOnlyList<Guid> SummativeAssessmentIds => _summativeAssessmentIds.AsReadOnly();
    public IReadOnlyList<Guid> FormativeAssessmentIds => _formativeAssessmentIds.AsReadOnly();
    public IReadOnlyList<LessonComment> Comments => _comments.AsReadOnly();

    public void AddLessonComment(LessonComment comment)
    {
        if (!_comments.Contains(comment))
        {
            _comments.Add(comment);
            UpdatedDateTime = DateTime.Now;
        }
    }

    public void AddResource(Resource resource)
    {
        if (!_resources.Contains(resource))
        {
            _resources.Add(resource);
            UpdatedDateTime = DateTime.Now;
        }
    }

    public void AddSummativeAssessment(Guid assessmentId)
    {
        if (!_summativeAssessmentIds.Contains(assessmentId))
        {
            _summativeAssessmentIds.Add(assessmentId);
            UpdatedDateTime = DateTime.Now;
        }
    }

    public void AddFormativeAssessment(Guid assessmentId)
    {
        if (!_formativeAssessmentIds.Contains(assessmentId))
        {
            _formativeAssessmentIds.Add(assessmentId);
            UpdatedDateTime = DateTime.Now;
        }
    }

    public void SetNumberOfPeriods(int newNumberOfPeriods)
    {
        if (newNumberOfPeriods != NumberOfPeriods)
        {
            NumberOfPeriods = newNumberOfPeriods;
            UpdatedDateTime = DateTime.Now;
        }
    }

    private LessonPlan(
        Guid id,
        Guid teacherId,
        Guid subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        int numberOfPeriods,
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
        NumberOfPeriods = numberOfPeriods;
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
        int numberOfPeriods,
        List<Resource>? resources,
        List<Guid>? summativeAssessmentIds,
        List<Guid>? formativeAssessmentIds)
    {
        return new LessonPlan(Guid.NewGuid(), teacherId, subjectId, planningNotes, startTime, endTime, numberOfPeriods, DateTime.UtcNow, DateTime.UtcNow, resources, summativeAssessmentIds, formativeAssessmentIds);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonPlan() { }
}
