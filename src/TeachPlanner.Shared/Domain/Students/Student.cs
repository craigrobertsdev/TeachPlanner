using TeachPlanner.Shared.Domain.Assessments;
using TeachPlanner.Shared.Domain.Common.Interfaces;
using TeachPlanner.Shared.Domain.Common.Primatives;
using TeachPlanner.Shared.Domain.Reports;
using TeachPlanner.Shared.Domain.Teachers;

namespace TeachPlanner.Shared.Domain.Students;

public sealed class Student : Entity<StudentId>, IAggregateRoot {
    private readonly List<Assessment> _assessments = new();
    private readonly List<Report> _reports = new();

    private Student(
        StudentId id,
        TeacherId teacherId,
        string firstName,
        string lastName) : base(id) {
        TeacherId = teacherId;
        FirstName = firstName;
        LastName = lastName;
    }

    public TeacherId TeacherId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }


    public IReadOnlyList<Report> Reports => _reports;
    public IReadOnlyList<Assessment> Assessments => _assessments;

    public static Student Create(TeacherId teacherId, string firstName, string lastName) {
        return new Student(
            new StudentId(Guid.NewGuid()),
            teacherId,
            firstName,
            lastName);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Student() {
    }
}