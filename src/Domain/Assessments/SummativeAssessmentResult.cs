using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.Assessments;

public class SummativeAssessmentResult : ValueObject
{
    public string Comments { get; private set; }
    public AssessmentGrade Grade { get; private set; }
    public DateTime DateMarked { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private SummativeAssessmentResult(
        string comments,
        AssessmentGrade grade,
        DateTime dateMarked)
    {
        Comments = comments;
        Grade = grade;
        DateMarked = dateMarked;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static SummativeAssessmentResult Create(
        string comments,
        AssessmentGrade grade,
        DateTime dateMarked)
    {
        return new SummativeAssessmentResult(
            comments,
            grade,
            dateMarked);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Comments;
        yield return Grade;
        yield return DateMarked;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SummativeAssessmentResult() { }
}
