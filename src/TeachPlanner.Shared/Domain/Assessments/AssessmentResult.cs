namespace TeachPlanner.Shared.Domain.Assessments;

public record AssessmentResult {
    private AssessmentResult(
        string comments,
        AssessmentGrade grade,
        DateTime dateMarked) {
        Comments = comments;
        Grade = grade;
        DateMarked = dateMarked;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public string Comments { get; private set; }
    public AssessmentGrade Grade { get; private set; }
    public DateTime DateMarked { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public static AssessmentResult Create(
        string comments,
        AssessmentGrade grade,
        DateTime dateMarked) {
        return new AssessmentResult(
            comments,
            grade,
            dateMarked);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private AssessmentResult() {
    }
}