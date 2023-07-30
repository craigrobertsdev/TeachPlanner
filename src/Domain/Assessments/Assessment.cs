using Domain.Assessments.ValueObjects;
using Domain.Common.Enums;
using Domain.Common.Primatives;
using Domain.StudentAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;

namespace Domain.Assessments;

/// <summary>
/// Each assessment is implemented as an isolated unit with associations to a single student, teacher, and subject.
/// 
/// I may need to implement a way of tracking which assesments were conducted as part of each round of assessments
/// for some sort of data collection
/// </summary>
public abstract class Assessment : AggregateRoot<AssessmentId>
{
    public TeacherIdForReference TeacherId { get; private set; }
    public SubjectIdForReference SubjectId { get; private set; }
    public StudentIdForReference StudentId { get; private set; }
    public YearLevelValue YearLevel { get; private set; }
    public DateTime ConductedDateTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    protected Assessment(
        AssessmentId id,
        TeacherIdForReference teacherId,
        SubjectIdForReference subjectId,
        StudentIdForReference studentId,
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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Assessment() { }
}
