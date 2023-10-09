using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.Students;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Domain.Assessments;

public class Assessment : Entity<AssessmentId>, IAggregateRoot
{
    public TeacherId TeacherId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public StudentId StudentId { get; private set; }
    public LessonPlanId LessonPlanId { get; private set; }
    public YearLevelValue YearLevel { get; private set; }
    public AssessmentType AssessmentType { get; private set; }
    public AssessmentResult? AssessmentResult { get; private set; }
    public string PlanningNotes { get; private set; }
    public DateTime ConductedDateTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    protected Assessment(
        AssessmentId id,
        TeacherId teacherId,
        SubjectId subjectId,
        StudentId studentId,
        LessonPlanId lessonPlanId,
        YearLevelValue yearLevel,
        string planningNotes,
        DateTime conductedDateTime) : base(id)
    {
        Id = id;
        TeacherId = teacherId;
        SubjectId = subjectId;
        StudentId = studentId;
        LessonPlanId = lessonPlanId;
        YearLevel = yearLevel;
        PlanningNotes = planningNotes;
        ConductedDateTime = conductedDateTime;
    }

    public void SetAssessmentResult(AssessmentResult result)
    {
        if (AssessmentResult is not null)
        {
            AssessmentResult = result;
            UpdatedDateTime = DateTime.Now;
        }
    }

    public Assessment Create(
        TeacherId teacherId,
        SubjectId subjectId,
        StudentId studentId,
        LessonPlanId lessonPlanId,
        YearLevelValue yearLevel,
        string planningNotes,
        DateTime conductedDateTime)
    {
        return new Assessment(
            new AssessmentId(Guid.NewGuid()),
            teacherId,
            subjectId,
            studentId,
            lessonPlanId,
            yearLevel,
            planningNotes,
            conductedDateTime);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Assessment() { }
}
