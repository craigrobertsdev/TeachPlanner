using Domain.Common.Assessment.ValueObjects;
using Domain.Common.Primatives;
using Domain.StudentAggregate.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;

namespace Domain.Common.Assessment;

public abstract class Assessment : AggregateRoot<AssessmentId>
{
    public TeacherId TeacherId { get; private set; }
    public StudentId StudentId { get; private set; }
    public string Comments { get; private set; }
    public DateTime AssessmentConductedDate { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    protected Assessment(
        AssessmentId id,
        TeacherId teacherId,
        StudentId studentId,
        string comments,
        DateTime assessmentConductedDate,
        DateTime createdDateTime,
        DateTime updatedDateTime) : base(id)
    {
        TeacherId = teacherId;
        StudentId = studentId;
        Comments = comments;
        AssessmentConductedDate = assessmentConductedDate;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }
}
