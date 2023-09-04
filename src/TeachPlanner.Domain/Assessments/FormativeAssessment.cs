using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Students;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Teachers;

namespace TeachPlanner.Domain.Assessments;

public class FormativeAssessment : Assessment
{
    public string Comments { get; private set; }

    private FormativeAssessment(
    Guid id,
        Guid teacherId,
        Guid subjectId,
        Guid studentId,
        YearLevelValue yearLevel,
        DateTime conductedDateTime,
        string? comments)
        : base(id, teacherId, subjectId, studentId, yearLevel, conductedDateTime)
    {
        Comments = comments ?? string.Empty;
    }

    public static FormativeAssessment Create(
        Guid teacherId,
        Guid subjectId,
        Guid studentId,
        YearLevelValue yearLevel,
        DateTime conductedDateTime,
        string? comments)
    {
        return new FormativeAssessment(
            Guid.NewGuid(),
            teacherId,
            subjectId,
            studentId,
            yearLevel,
            conductedDateTime,
            comments);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private FormativeAssessment() { }
}
