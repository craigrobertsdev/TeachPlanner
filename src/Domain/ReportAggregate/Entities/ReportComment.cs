using Domain.Common.Enums;
using Domain.Common.Primatives;
using Domain.ReportAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;

namespace Domain.ReportAggregate.Entities;

public sealed class ReportComment : Entity<ReportCommentId>
{
    public SubjectId SubjectId { get; private set; }
    public Grade Grade { get; private set; }
    public string Comments { get; private set; }
    public int CharacterLimit { get; private set; }

    private ReportComment(
        ReportCommentId id,
        SubjectId subjectId,
        Grade grade,
        string comments,
        int characterLimit) : base(id)
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
            ReportCommentId.Create(),
            subjectId,
            grade,
            comments,
            characterLimit);
    }

#pragma warning disable CS8618
    private ReportComment() { }
}
