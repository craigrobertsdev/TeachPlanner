using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Students;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Domain.Assessments;

public class SummativeAssessment : Assessment
{
    public string PlanningNotes { get; private set; }
    public DateTime DateScheduled { get; private set; }
    public SummativeAssessmentResult Result { get; private set; }

    private SummativeAssessment(
          Guid id,
        Guid teacherId,
        Guid subjectId,
        Guid studentId,
        YearLevelValue yearLevel,
        string planningNotes,
        DateTime conductedDateTime,
        DateTime dateScheduled,
        SummativeAssessmentResult result)
        : base(id, teacherId, subjectId, studentId, yearLevel, conductedDateTime)
    {
        PlanningNotes = planningNotes;
        DateScheduled = dateScheduled;
        Result = result;
    }

    public static SummativeAssessment Create(
        Guid teacherId,
        Guid subjectId,
        Guid studentId,
        YearLevelValue yearLevel,
        string planningNotes,
        DateTime conductedDateTime,
        DateTime dateScheduled,
        SummativeAssessmentResult result)
    {
        return new SummativeAssessment(
            Guid.NewGuid(),
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
