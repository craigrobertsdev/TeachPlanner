﻿using TeachPlanner.Api.Entities.Assessments;
using TeachPlanner.Api.Entities.Common.Interfaces;
using TeachPlanner.Api.Entities.Common.Primatives;
using TeachPlanner.Api.Entities.Reports;
using TeachPlanner.Api.Entities.Teachers;

namespace TeachPlanner.Api.Entities.Students;

public sealed class Student : Entity<StudentId>, IAggregateRoot
{
    private readonly List<Report> _reports = new();
    private readonly List<Assessment> _assessments = new();
    public TeacherId TeacherId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }


    public IReadOnlyList<Report> Reports => _reports;
    public IReadOnlyList<Assessment> Assessments => _assessments;

    private Student(
        StudentId id,
        TeacherId teacherId,
        string firstName,
        string lastName) : base(id)
    {
        TeacherId = teacherId;
        FirstName = firstName;
        LastName = lastName;
    }

    public static Student Create(TeacherId teacherId, string firstName, string lastName)
    {
        return new Student(
            new StudentId(Guid.NewGuid()),
            teacherId,
            firstName,
            lastName);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Student() { }
}
