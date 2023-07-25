using Domain.Common.Enums;
using Domain.Common.Primatives;
using Domain.ReportAggregate.ValueObjects;

namespace Domain.ReportAggregate.Entities;

public sealed class ReportComment : Entity<ReportCommentId>
{
    public Grade Grade { get; private set; }
    public string Comments { get; private set; }
    public int CharacterLimit { get; private set; }

    private ReportComment(
        ReportCommentId id,
        Grade grade,
        string comments,
        int characterLimit) : base(id)
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
            new ReportCommentId(Guid.NewGuid()),
            grade,
            comments,
            characterLimit);
    }

#pragma warning disable CS8618
    private ReportComment() { }
}
