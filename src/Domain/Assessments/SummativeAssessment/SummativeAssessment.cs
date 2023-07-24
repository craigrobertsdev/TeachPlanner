using Domain.Assessments.SummativeAssessment.Entities;
using Domain.Assessments.SummativeAssessment.ValueObjects;
using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Enums;
using Domain.StudentAggregate.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;

namespace Domain.Assessments.SummativeAssessment;

public class SummativeAssessment : Assessment
{
    public string PlanningNotes { get; private set; }
    public DateTime DateScheduled { get; private set; }
    public SummativeAssessmentResult Result { get; private set; }

    private SummativeAssessment(
        AssessmentId id,
        TeacherId teacherId,
        SubjectId subjectId,
        StudentId studentId,
        YearLevelValue yearLevel,
        string planningNotes,
        DateTime conductedDateTime,
        DateTime dateScheduled,
        SummativeAssessmentResult result) : base(
            id,
            teacherId,
            subjectId,
            studentId,
            yearLevel,
            conductedDateTime)
    {
        PlanningNotes = planningNotes;
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
            new AssessmentId(Guid.NewGuid()),
            teacherId,
            subjectId,
            studentId,
            yearLevel,
            planningNotes,
            conductedDateTime,
            dateScheduled,
            result);
    }
}
