﻿using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.Reports;

public sealed class Report : AggregateRoot
{
    private readonly List<ReportComment> _reportComments = new();
    public Guid TeacherId { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid SubjectId { get; private set; }
    public Guid YearDataId { get; private set; }
    public YearLevelValue YearLevel { get; private set; }
    public IReadOnlyList<ReportComment> ReportComments => _reportComments.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Report(
        Guid id,
        List<ReportComment> reportComments,
        Guid teacherId,
        Guid studentId,
        Guid yearDataId,
        Guid subjectId,
        YearLevelValue yearLevel,
        DateTime createdDateTime,
        DateTime updatedDateTime) : base(id)
    {
        _reportComments = reportComments;
        TeacherId = teacherId;
        StudentId = studentId;
        YearDataId = yearDataId;
        SubjectId = subjectId;
        YearLevel = yearLevel;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Report Create(
        Guid teacherId,
        Guid studentId,
        Guid yearDataId,
        Guid subjectId,
        YearLevelValue yearLevel,
        DateTime createdDateTime,
        DateTime updatedDateTime)
    {
        return new Report(
            Guid.NewGuid(),
            new List<ReportComment>(),
            teacherId,
            studentId,
            yearDataId,
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
