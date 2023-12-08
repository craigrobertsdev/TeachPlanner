using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;

namespace TeachPlanner.Api.Domain.LessonPlans;

public sealed class LessonPlan : Entity<LessonPlanId>, IAggregateRoot {
    private readonly List<Assessment> _assessments = new();
    private readonly List<LessonComment> _comments = new();
    private readonly List<string> _curriculumCodes = new();
    private readonly List<LessonPlanResource> _lessonPlanResources = new();

    private LessonPlan(
        LessonPlanId id,
        YearDataId yearDataId,
        SubjectId subjectId,
        List<string> curriculumCodes,
        string planningNotes,
        int numberOfPeriods,
        int startPeriod,
        DateOnly lessonDate,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<LessonPlanResource>? lessonPlanResources,
        List<Assessment>? assessments) : base(id) {
        YearDataId = yearDataId;
        SubjectId = subjectId;
        _curriculumCodes = curriculumCodes;
        PlanningNotes = planningNotes;
        NumberOfPeriods = numberOfPeriods;
        StartPeriod = startPeriod;
        LessonDate = lessonDate;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;

        if (lessonPlanResources != null) _lessonPlanResources = lessonPlanResources;
        if (assessments != null) _assessments = assessments;
    }

    public YearDataId YearDataId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public string PlanningNotes { get; private set; }
    public DateOnly LessonDate { get; private set; }
    public int NumberOfPeriods { get; private set; }
    public int StartPeriod { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public IReadOnlyList<LessonPlanResource> LessonPlanResources => _lessonPlanResources.AsReadOnly();
    public IReadOnlyList<Assessment> Assessments => _assessments.AsReadOnly();
    public IReadOnlyList<LessonComment> Comments => _comments.AsReadOnly();
    public IReadOnlyList<string> CurriculumCodes => _curriculumCodes.AsReadOnly();


    public void AddLessonComment(LessonComment comment) {
        if (!_comments.Contains(comment)) {
            _comments.Add(comment);
            UpdatedDateTime = DateTime.Now;
        }
    }

    public void AddResource(ResourceId resourceId) {
        if (!_lessonPlanResources.Any(lr => lr.ResourceId == resourceId)) {
            _lessonPlanResources.Add(LessonPlanResource.Create(Id, resourceId));
            UpdatedDateTime = DateTime.Now;
        }
    }

    public void AddAssessment(Assessment assessment) {
        if (!_assessments.Contains(assessment)) {
            _assessments.Add(assessment);
            UpdatedDateTime = DateTime.Now;
        }
    }

    public void SetNumberOfPeriods(int newNumberOfPeriods) {
        if (newNumberOfPeriods != NumberOfPeriods) {
            NumberOfPeriods = newNumberOfPeriods;
            UpdatedDateTime = DateTime.Now;
        }
    }

    public static LessonPlan Create(
        YearDataId yearDataId,
        SubjectId subjectId,
        List<string> curriculumCodes,
        string planningNotes,
        int numberOfPeriods,
        int startPeriod,
        DateOnly lessonDate,
        List<LessonPlanResource>? lessonPlanResources,
        List<Assessment>? assessments) {
        return new LessonPlan(
            new LessonPlanId(Guid.NewGuid()),
            yearDataId,
            subjectId,
            curriculumCodes,
            planningNotes,
            numberOfPeriods,
            startPeriod,
            lessonDate,
            DateTime.UtcNow,
            DateTime.UtcNow,
            lessonPlanResources,
            assessments);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LessonPlan() {
    }
}