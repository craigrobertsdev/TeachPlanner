using TeachPlanner.Shared.Domain.Common.Enums;
using TeachPlanner.Shared.Domain.Common.Interfaces;
using TeachPlanner.Shared.Domain.Common.Primatives;
using TeachPlanner.Shared.Domain.YearDataRecords;
using TeachPlanner.Shared.Domain.Students;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.Curriculum;

namespace TeachPlanner.Shared.Domain.Reports;

public sealed class Report : Entity<ReportId>, IAggregateRoot {
    private readonly List<ReportComment> _reportComments = new();

    private Report(
        ReportId id,
        List<ReportComment> reportComments,
        TeacherId teacherId,
        StudentId studentId,
        SubjectId subjectId,
        YearLevelValue yearLevel,
        DateTime createdDateTime,
        DateTime updatedDateTime) : base(id) {
        _reportComments = reportComments;
        TeacherId = teacherId;
        StudentId = studentId;
        SubjectId = subjectId;
        YearLevel = yearLevel;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public TeacherId TeacherId { get; private set; }
    public StudentId StudentId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public YearLevelValue YearLevel { get; private set; }
    public IReadOnlyList<ReportComment> ReportComments => _reportComments.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public static Report Create(
        TeacherId teacherId,
        StudentId studentId,
        YearDataId yearDataId,
        SubjectId subjectId,
        YearLevelValue yearLevel,
        DateTime createdDateTime,
        DateTime updatedDateTime) {
        return new Report(
            new ReportId(Guid.NewGuid()),
            new List<ReportComment>(),
            teacherId,
            studentId,
            subjectId,
            yearLevel,
            createdDateTime,
            updatedDateTime);
    }

    public void AddReportComment(ReportComment reportComment) {
        _reportComments.Add(reportComment);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Report() {
    }
}