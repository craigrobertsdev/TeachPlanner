using Domain.Common.Enums;
using Domain.Common.Primatives;
using Domain.StudentAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;

namespace Domain.Assessments;

public class FormativeAssessment : AggregateRoot<FormativeAssessmentId>
{
    public TeacherId TeacherId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public StudentId StudentId { get; private set; }
    public YearLevelValue YearLevel { get; private set; }
    public DateTime ConductedDateTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }
    public string Comments { get; private set; }

    private FormativeAssessment(
        FormativeAssessmentId id,
        TeacherId teacherId,
        SubjectId subjectId,
        StudentId studentId,
        YearLevelValue yearLevel,
        DateTime conductedDateTime,
        string? comments) : base(id)
    {
        TeacherId = teacherId;
        SubjectId = subjectId;
        StudentId = studentId;
        YearLevel = yearLevel;
        ConductedDateTime = conductedDateTime;
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
            new FormativeAssessmentId(Guid.NewGuid()),
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
