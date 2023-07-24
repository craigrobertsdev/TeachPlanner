using Domain.Assessments.SummativeAssessment.ValueObjects;
using Domain.Common.Curriculum.ValueObjects;
using Domain.Common.Enums;
using Domain.Common.Primatives;
using Domain.StudentAggregate.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;

namespace Domain.Assessments;

public abstract class Assessment : AggregateRoot<AssessmentId>
{
    public TeacherId TeacherId { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public StudentId StudentId { get; private set; }
    public YearLevelValue YearLevel { get; private set; }
    public DateTime ConductedDateTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }
    protected Assessment(
        AssessmentId id,
        TeacherId teacherId,
        SubjectId subjectId,
        StudentId studentId,
        YearLevelValue yearLevel,
        DateTime conductedDateTime) : base(id)
    {
        TeacherId = teacherId;
        SubjectId = subjectId;
        StudentId = studentId;
        YearLevel = yearLevel;
        ConductedDateTime = conductedDateTime;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }
}
