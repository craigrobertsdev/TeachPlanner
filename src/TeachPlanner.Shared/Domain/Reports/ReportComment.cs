using TeachPlanner.Shared.Domain.Common.Enums;

namespace TeachPlanner.Shared.Domain.Reports;

public record ReportComment {
    private ReportComment(
        Grade grade,
        string comments,
        int characterLimit) {
        Grade = grade;
        Comments = comments;
        CharacterLimit = characterLimit;
    }

    public Grade Grade { get; private set; }
    public string Comments { get; private set; }
    public int CharacterLimit { get; private set; }

    public static ReportComment Create(
        Grade grade,
        string comments,
        int characterLimit) {
        return new ReportComment(
            grade,
            comments,
            characterLimit);
    }
#pragma warning disable CS8618
    private ReportComment() {
    }
}