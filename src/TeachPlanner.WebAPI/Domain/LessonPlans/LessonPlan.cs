using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.Resources;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Domain.LessonPlans;

public sealed class LessonPlan : Entity<LessonPlanId>, IAggregateRoot
{
    private readonly List<LessonPlanResource> _lessonPlanResources = new();
    private readonly List<Assessment> _assessments = new();
    private readonly List<LessonComment> _comments = new();
    public TeacherId TeacherId { get; private set; }
    public YearDataId YearDataId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public string PlanningNotes { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }
    public int NumberOfPeriods { get; private set; }

    public IReadOnlyList<LessonPlanResource> LessonPlanResources => _lessonPlanResources.AsReadOnly();
    public IReadOnlyList<Assessment> Assessments => _assessments.AsReadOnly();
    public IReadOnlyList<LessonComment> Comments => _comments.AsReadOnly();

    public void AddLessonComment(LessonComment comment)
    {
        if (!_comments.Contains(comment))
        {
            _comments.Add(comment);
            UpdatedDateTime = DateTime.Now;
        }
    }

    public void AddResource(ResourceId resourceId)
    {
        if (!_lessonPlanResources.Any(lr => lr.ResourceId == resourceId))
        {
            _lessonPlanResources.Add(LessonPlanResource.Create(Id, resourceId));
            UpdatedDateTime = DateTime.Now;
        }
    }

    public void AddAssessment(Assessment assessment)
    {
        if (!_assessments.Contains(assessment))
        {
            _assessments.Add(assessment);
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
        LessonPlanId id,
        TeacherId teacherId,
        YearDataId yearDataId,
        SubjectId subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        int numberOfPeriods,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<Assessment>? assessments) : base(id)
    {
        TeacherId = teacherId;
        YearDataId = yearDataId;
        SubjectId = subjectId;
        PlanningNotes = planningNotes;
        StartTime = startTime;
        EndTime = endTime;
        NumberOfPeriods = numberOfPeriods;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;

        if (assessments != null)
        {
            _assessments = assessments;
        }
    }

    public static LessonPlan Create(
        TeacherId teacherId,
        YearDataId yearDataId,
        SubjectId subjectId,
        string planningNotes,
        DateTime startTime,
        DateTime endTime,
        int numberOfPeriods,
        List<Assessment>? assessments
        )
    {
        return new LessonPlan(
            new LessonPlanId(Guid.NewGuid()),
            teacherId,
            yearDataId,
            subjectId,
            planningNotes,
            startTime,
            endTime,
            numberOfPeriods,
            DateTime.UtcNow,
            DateTime.UtcNow,
            assessments);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonPlan() { }
}
