using TeachPlanner.Domain.Assessments;
using TeachPlanner.Domain.Common.Primatives;
using TeachPlanner.Domain.LessonPlans;
using TeachPlanner.Domain.Reports;
using TeachPlanner.Domain.Resources;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Students;
using TeachPlanner.Domain.WeekPlanners;
using TeachPlanner.Domain.Common.Enums;

namespace TeachPlanner.Domain.Teachers;

public sealed class Teacher : AggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    private readonly List<Subject> _subjectsTaught = new();
    private readonly List<Student> _students = new();
    private readonly List<SummativeAssessment> _summativeAssessments = new();
    private readonly List<FormativeAssessment> _formativeAssessments = new();
    private readonly List<Resource> _resources = new();
    private readonly List<Report> _reports = new();
    private readonly List<LessonPlan> _lessonPlans = new();
    private readonly List<WeekPlanner> _weekPlanners = new();
    public IReadOnlyList<Subject> SubjectsTaught => _subjectsTaught;
    public IReadOnlyList<Student> Students => _students;
    public IReadOnlyList<SummativeAssessment> SummativeAssessments => _summativeAssessments;
    public IReadOnlyList<FormativeAssessment> FormativeAssessments => _formativeAssessments;
    public IReadOnlyList<Resource> Resources => _resources;
    public IReadOnlyList<Report> Reports => _reports;
    public IReadOnlyList<LessonPlan> LessonPlans => _lessonPlans;
    public IReadOnlyList<WeekPlanner> WeekPlanners => _weekPlanners;

    private Teacher(Guid id, string firstName, string lastName, string email, string password) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public static Teacher Create(string firstName, string lastName, string email, string password)
    {
        return new(Guid.NewGuid(), firstName, lastName, email, password);
    }

    public void AddSubjectsTaught(List<Subject> subjects)
    {
        foreach (var subject in subjects)
        {
            if (_subjectsTaught.Find(s => s.Id == subject.Id) == null)
            {
                _subjectsTaught.Add(subject);
            }
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Teacher() { }
}
