using TeachPlanner.Api.Entities.Common.Enums;
using TeachPlanner.Api.Entities.Common.Interfaces;
using TeachPlanner.Api.Entities.Common.Primatives;
using TeachPlanner.Api.Entities.Students;
using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.Teachers;

namespace TeachPlanner.Api.Entities.Assessments;

public class Assessment : Entity<AssessmentId>, IAggregateRoot
{
    public TeacherId TeacherId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public StudentId StudentId { get; private set; }
    public YearLevelValue YearLevel { get; private set; }
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
        YearLevelValue yearLevel,
        string planningNotes,
        DateTime conductedDateTime) : base(id)
    {
        Id = id;
        TeacherId = teacherId;
        SubjectId = subjectId;
        StudentId = studentId;
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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Assessment() { }
}
