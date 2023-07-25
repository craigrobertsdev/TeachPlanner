using Domain.Assessments.ValueObjects;
using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Enums;
using Domain.StudentAggregate.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;

namespace Domain.Assessments;

public class FormativeAssessment : Assessment
{
    public string Comments { get; private set; }

    private FormativeAssessment(
        AssessmentId id,
        TeacherId teacherId,
        SubjectId subjectId,
        StudentId studentId,
        YearLevelValue yearLevel,
        DateTime conductedDateTime,
        string? comments) : base(
            id,
            teacherId,
            subjectId,
            studentId,
            yearLevel,
            conductedDateTime)
    {
        Comments = comments ?? string.Empty;
    }

    public static FormativeAssessment Create(
        TeacherId teacherId,
        SubjectId subjectId,
        StudentId studentId,
        YearLevelValue yearLevel,
        DateTime conductedDateTime,
        string? comments)
    {
        return new FormativeAssessment(
            new AssessmentId(Guid.NewGuid()),
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
