using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.Subjects;

namespace TeachPlanner.Api.Domain.Reports;

public sealed class ReportComment : ValueObject
{
    public Grade Grade { get; private set; }
    public string Comments { get; private set; }
    public int CharacterLimit { get; private set; }

    private ReportComment(
        Grade grade,
        string comments,
        int characterLimit)
    {
        Grade = grade;
        Comments = comments;
        CharacterLimit = characterLimit;
    }

    public static ReportComment Create(
        Grade grade,
        string comments,
        int characterLimit)
    {
        return new ReportComment(
            grade,
            comments,
            characterLimit);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Grade;
        yield return Comments;
        yield return CharacterLimit;
    }

#pragma warning disable CS8618
    private ReportComment() { }
}
