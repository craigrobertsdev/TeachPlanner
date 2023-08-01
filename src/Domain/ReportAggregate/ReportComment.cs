using Domain.Common.Enums;
using Domain.Common.Primatives;
using Domain.SubjectAggregates;

namespace Domain.ReportAggregate;

public sealed class ReportComment : ValueObject
{
    public SubjectId SubjectId { get; private set; }
    public Grade Grade { get; private set; }
    public string Comments { get; private set; }
    public int CharacterLimit { get; private set; }

    private ReportComment(
        SubjectId subjectId,
        Grade grade,
        string comments,
        int characterLimit)
    {
        SubjectId = subjectId;
        Grade = grade;
        Comments = comments;
        CharacterLimit = characterLimit;
    }

    public static ReportComment Create(
        Grade grade,
        SubjectId subjectId,
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
        yield return SubjectId;
        yield return Grade;
        yield return Comments;
        yield return CharacterLimit;
    }

#pragma warning disable CS8618
    private ReportComment() { }
}
