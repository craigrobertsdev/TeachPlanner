using Domain.Assessments.SummativeAssessment.ValueObjects;
using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Primatives;
using Domain.StudentAggregate.ValueObjects;

namespace Domain.Assessments.SummativeAssessment.Entities;

public class SummativeAssessmentResult : Entity<SummativeAssessmentResultId>
{
    public SummativeAssessmentId SummativeAssessmentId { get; private set; }
    public AssessmentId AssessmentId { get; private set; }
    public StudentId StudentId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public string Comments { get; private set; }
    public AssessmentGrade Grade { get; private set; }
    public DateTime DateMarked { get; private set; }

    private SummativeAssessmentResult(
        SummativeAssessmentResultId id,
        SummativeAssessmentId summativeAssessmentId,
        AssessmentId assessmentId,
        StudentId studentId,
        SubjectId subjectId,
        string comments,
        AssessmentGrade grade,
        DateTime dateMarked) : base(id)
    {
        SummativeAssessmentId = summativeAssessmentId;
        AssessmentId = assessmentId;
        StudentId = studentId;
        SubjectId = subjectId;
        Comments = comments;
        Grade = grade;
        DateMarked = dateMarked;
    }

    public static SummativeAssessmentResult Create(
        SummativeAssessmentId summativeAssessmentId,
        AssessmentId assessmentId,
        StudentId studentId,
        SubjectId subjectId,
        string comments,
        AssessmentGrade grade,
        DateTime dateMarked)
    {
        return new SummativeAssessmentResult(
            new SummativeAssessmentResultId(Guid.NewGuid()),
            summativeAssessmentId,
            assessmentId,
            studentId,
            subjectId,
            comments,
            grade,
            dateMarked);
    }
}
