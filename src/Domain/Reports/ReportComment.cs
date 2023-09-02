using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.Reports;

public sealed class ReportComment : ValueObject
{
    public Guid Guid { get; private set; }
    public Grade Grade { get; private set; }
    public string Comments { get; private set; }
    public int CharacterLimit { get; private set; }

    private ReportComment(
        Guid subjectId,
        Grade grade,
        string comments,
        int characterLimit)
    {
        Guid = subjectId;
        Grade = grade;
        Comments = comments;
        CharacterLimit = characterLimit;
    }

    public static ReportComment Create(
        Grade grade,
        Guid subjectId,
        string comments,
        int characterLimit)
    {
        return new ReportComment(
            subjectId,
            grade,
            comments,
            characterLimit);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Guid;
        yield return Grade;
        yield return Comments;
        yield return CharacterLimit;
    }

#pragma warning disable CS8618
    private ReportComment() { }
}
