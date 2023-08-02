﻿using Domain.Common.Enums;
using Domain.Common.Primatives;

namespace Domain.Assessments;

public abstract class Assessment : AggregateRoot
{
    public Guid TeacherId { get; private set; }
    public Guid SubjectId { get; private set; }
    public Guid StudentId { get; private set; }
    public YearLevelValue YearLevel { get; private set; }
    public DateTime ConductedDateTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    protected Assessment(
        Guid id,
        Guid teacherId,
        Guid subjectId,
        Guid studentId,
        YearLevelValue yearLevel,
        DateTime conductedDateTime) : base(id)
    {
        TeacherId = teacherId;
        SubjectId = subjectId;
        StudentId = studentId;
        YearLevel = yearLevel;
        ConductedDateTime = conductedDateTime;
    }

    protected Assessment() { }
}
