using Domain.Assessments.ValueObjects;
using Domain.Common.Primatives;
using Domain.StudentAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;

namespace Domain.Assessments.Entities;

public class SummativeAssessmentResult : Entity<SummativeAssessmentResultId>
{
    public StudentId StudentId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public string Comments { get; private set; }
    public AssessmentGrade Grade { get; private set; }
    public DateTime DateMarked { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private SummativeAssessmentResult(
        SummativeAssessmentResultId id,
        StudentId studentId,
        SubjectId subjectId,
        string comments,
        AssessmentGrade grade,
        DateTime dateMarked) : base(id)
    {
        StudentId = studentId;
        SubjectId = subjectId;
        Comments = comments;
        Grade = grade;
        DateMarked = dateMarked;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static SummativeAssessmentResult Create(
        StudentId studentId,
        SubjectId subjectId,
        string comments,
        AssessmentGrade grade,
        DateTime dateMarked)
    {
        return new SummativeAssessmentResult(
            SummativeAssessmentResultId.Create(),
            studentId,
            subjectId,
            comments,
            grade,
            dateMarked);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SummativeAssessmentResult() { }
}
