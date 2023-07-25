using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Enums;
using Domain.Common.Primatives;
using Domain.ReportAggregate.Entities;
using Domain.ReportAggregate.ValueObjects;
using Domain.StudentAggregate.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;

namespace Domain.ReportAggregate;

public class Report : AggregateRoot<ReportId>
{
    private readonly Dictionary<SubjectId, ReportComment> _subjectGrades = new();
    public TeacherId TeacherId { get; private set; }
    public StudentId StudentId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public YearLevelValue YearLevel { get; private set; }
    public IReadOnlyDictionary<SubjectId, ReportComment> SubjectGrades => _subjectGrades.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Report(
        ReportId id,
        Dictionary<SubjectId, ReportComment> subjectGrades,
        TeacherId teacherId,
        StudentId studentId,
        SubjectId subjectId,
        YearLevelValue yearLevel,
        DateTime createdDateTime,
        DateTime updatedDateTime) : base(id)
    {
        _subjectGrades = subjectGrades;
        TeacherId = teacherId;
        StudentId = studentId;
        SubjectId = subjectId;
        YearLevel = yearLevel;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Report Create(
        TeacherId teacherId,
        StudentId studentId,
        SubjectId subjectId,
        YearLevelValue yearLevel,
        DateTime createdDateTime,
        DateTime updatedDateTime)
    {
        return new Report(
            new ReportId(Guid.NewGuid()),
            new Dictionary<SubjectId, ReportComment>(),
            teacherId,
            studentId,
            subjectId,
            yearLevel,
            createdDateTime,
            updatedDateTime);
    }

    public void AddSubjectGrade(SubjectId subjectId, ReportComment reportComment)
    {
        _subjectGrades.Add(subjectId, reportComment);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Report() { }
}
