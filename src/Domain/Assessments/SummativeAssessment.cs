using Domain.Common.Enums;
using Domain.Common.Primatives;
using Domain.StudentAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;

namespace Domain.Assessments;

public class SummativeAssessment : AggregateRoot<SummativeAssessmentId>
{
    public TeacherId TeacherId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public StudentId StudentId { get; private set; }
    public YearLevelValue YearLevel { get; private set; }
    public DateTime ConductedDateTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }
    public string PlanningNotes { get; private set; }
    public DateTime DateScheduled { get; private set; }
    public SummativeAssessmentResult Result { get; private set; }

    private SummativeAssessment(
        SummativeAssessmentId id,
        TeacherId teacherId,
        SubjectId subjectId,
        StudentId studentId,
        YearLevelValue yearLevel,
        string planningNotes,
        DateTime conductedDateTime,
        DateTime dateScheduled,
        SummativeAssessmentResult result) : base(id)
    {
        TeacherId = teacherId;
        SubjectId = subjectId;
        StudentId = studentId;
        YearLevel = yearLevel;
        PlanningNotes = planningNotes;
        ConductedDateTime = conductedDateTime;
        DateScheduled = dateScheduled;
        Result = result;
    }

    public static SummativeAssessment Create(
        TeacherId teacherId,
        SubjectId subjectId,
        StudentId studentId,
        YearLevelValue yearLevel,
        string planningNotes,
        DateTime conductedDateTime,
        DateTime dateScheduled,
        SummativeAssessmentResult result)
    {
        return new SummativeAssessment(
            new SummativeAssessmentId(Guid.NewGuid()),
            teacherId,
            subjectId,
            studentId,
            yearLevel,
            planningNotes,
            conductedDateTime,
            dateScheduled,
            result);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SummativeAssessment() { }
}
