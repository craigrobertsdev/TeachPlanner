﻿using Domain.Assessments;
using Domain.Common.Primatives;
using Domain.ReportAggregate;
using Domain.TeacherAggregate;

namespace Domain.StudentAggregate;

public sealed class Student : AggregateRoot
{
    private readonly List<Guid> _reportIds = new();
    private readonly List<Guid> _summativeAssessmentIds = new();
    private readonly List<Guid> _formativeAssessmentIds = new();
    public TeacherId? TeacherId { get; private set; }

    public IReadOnlyList<Guid> ReportIds => _reportIds;
    public IReadOnlyList<Guid> SummativeAssessmentIds => _summativeAssessmentIds;
    public IReadOnlyList<Guid> FormativeAssessmentIds => _formativeAssessmentIds;

    private Student(Guid id) : base(id)
    {
    }

    public static Student Create()
    {
        return new Student(Guid.NewGuid());
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Student() { }
}
