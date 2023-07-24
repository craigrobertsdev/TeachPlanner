using Domain.Assessments.SummativeAssessment.ValueObjects;
using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Enums;
using Domain.StudentAggregate.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;

namespace Domain.Assessments.SummativeAssessment.Entities;

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
}
