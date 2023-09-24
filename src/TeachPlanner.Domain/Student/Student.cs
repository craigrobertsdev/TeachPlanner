using TeachPlanner.Domain.Assessments;
using TeachPlanner.Domain.Common.Primatives;
using TeachPlanner.Domain.Reports;

namespace TeachPlanner.Domain.Students;

public sealed class Student : AggregateRoot
{
    private readonly List<Report> _reports = new();
    private readonly List<Assessment> _assessments = new();
    public Guid TeacherId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }


    public IReadOnlyList<Report> Reports => _reports;
    public IReadOnlyList<Assessment> Assessments => _assessments;

    private Student(Guid id, Guid teacherId, string firstName, string lastName) : base(id)
    {
        TeacherId = teacherId;
        FirstName = firstName;
        LastName = lastName;
    }

    public static Student Create(Guid teacherId, string firstName, string lastName)
    {
        return new Student(Guid.NewGuid(), teacherId, firstName, lastName);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Student() { }
}
