using TeachPlanner.Domain.Assessments;
using TeachPlanner.Domain.Common.Primatives;
using TeachPlanner.Domain.LessonPlans;
using TeachPlanner.Domain.Reports;
using TeachPlanner.Domain.Resources;
using TeachPlanner.Domain.Students;
using TeachPlanner.Domain.WeekPlanners;
using TeachPlanner.Domain.TermPlanners;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.Users;

namespace TeachPlanner.Domain.Teachers;

public sealed class Teacher : AggregateRoot
{
    private readonly List<Guid> _subjectsTaughtIds = new();
    private readonly List<SummativeAssessment> _summativeAssessments = new();
    private readonly List<FormativeAssessment> _formativeAssessments = new();
    private readonly List<Resource> _resources = new();
    private readonly List<Report> _reports = new();
    private readonly List<LessonPlan> _lessonPlans = new();
    private readonly List<WeekPlanner> _weekPlanners = new();
    private readonly List<TermPlanner> _termPlanners = new();
    private readonly List<StudentsForYear> _studentsForYear = new();
    public Guid UserId { get; private set; } = Guid.Empty;
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public IReadOnlyList<Guid> SubjectsTaughtIds => _subjectsTaughtIds;
    public IReadOnlyList<SummativeAssessment> SummativeAssessments => _summativeAssessments;
    public IReadOnlyList<FormativeAssessment> FormativeAssessments => _formativeAssessments;
    public IReadOnlyList<Resource> Resources => _resources;
    public IReadOnlyList<Report> Reports => _reports;
    public IReadOnlyList<LessonPlan> LessonPlans => _lessonPlans;
    public IReadOnlyList<WeekPlanner> WeekPlanners => _weekPlanners;
    public IReadOnlyList<TermPlanner> TermPlanners => _termPlanners;
    public IReadOnlyList<StudentsForYear> StudentsForYear => _studentsForYear;

    private Teacher(Guid id, string firstName, string lastName) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Teacher Create(Guid userId, string firstName, string lastName)
    {
        return new(Guid.NewGuid(), firstName, lastName);
    }

    public void AddUserId(Guid userId)
    {
        if (UserId == Guid.Empty)
        {
            UserId = userId;
        }
    }

    public StudentsForYear? GetStudentsForYear(int year)
    {
        return _studentsForYear.FirstOrDefault(s => s.CalendarYear == year);
    }

    public void AddSubjectsTaught(List<Subject> subjects)
    {
        foreach (var subject in subjects)
        {
            if (NotInSubjectsTaught(subject.Id) && subject.Is)
            {
                _subjectsTaughtIds.Add(subject.Id);
            }
        }
    }

    private bool NotInSubjectsTaught(Guid subjectId)
    {
        foreach (var id in _subjectsTaughtIds)
        {
            if (subjectId == id) return true;
        }

        return false;
    }

    public void UpdateSubjectsTaught(Guid subjectId)
    {
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Teacher() { }
}
