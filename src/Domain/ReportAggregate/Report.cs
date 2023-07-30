using Domain.Common.Enums;
using Domain.Common.Primatives;
using Domain.ReportAggregate.Entities;
using Domain.ReportAggregate.ValueObjects;
using Domain.StudentAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;

namespace Domain.ReportAggregate;

public sealed class Report : AggregateRoot<ReportId>
{
    private readonly List<ReportComment> _reportComments = new();
    public TeacherIdForReference TeacherId { get; private set; }
    public StudentIdForReference StudentId { get; private set; }
    public SubjectIdForReference SubjectId { get; private set; }
    public YearLevelValue YearLevel { get; private set; }
    public IReadOnlyList<ReportComment> ReportComments => _reportComments.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Report(
        ReportId id,
        List<ReportComment> reportComments,
        TeacherIdForReference teacherId,
        StudentIdForReference studentId,
        SubjectIdForReference subjectId,
        YearLevelValue yearLevel,
        DateTime createdDateTime,
        DateTime updatedDateTime) : base(id)
    {
        _reportComments = reportComments;
        TeacherId = teacherId;
        StudentId = studentId;
        SubjectId = subjectId;
        YearLevel = yearLevel;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Report Create(
        TeacherIdForReference teacherId,
        StudentIdForReference studentId,
        SubjectIdForReference subjectId,
        YearLevelValue yearLevel,
        DateTime createdDateTime,
        DateTime updatedDateTime)
    {
        return new Report(
            ReportId.Create(),
            new List<ReportComment>(),
            teacherId,
            studentId,
            subjectId,
            yearLevel,
            createdDateTime,
            updatedDateTime);
    }

    public void AddReportComment(ReportComment reportComment)
    {
        _reportComments.Add(reportComment);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Report() { }
}
