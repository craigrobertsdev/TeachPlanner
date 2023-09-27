using TeachPlanner.Domain.Assessments;
using TeachPlanner.Domain.Common.Primatives;
using TeachPlanner.Domain.LessonPlans;
using TeachPlanner.Domain.Reports;
using TeachPlanner.Domain.Resources;
using TeachPlanner.Domain.Students;
using TeachPlanner.Domain.WeekPlanners;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.YearDataRecord;

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
    private readonly List<YearData> _yearDataHistory = new();
    public Guid UserId { get; private set; } = Guid.Empty;
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public IReadOnlyList<Guid> SubjectsTaughtIds => _subjectsTaughtIds.AsReadOnly();
    public IReadOnlyList<SummativeAssessment> SummativeAssessments => _summativeAssessments.AsReadOnly();
    public IReadOnlyList<FormativeAssessment> FormativeAssessments => _formativeAssessments.AsReadOnly();
    public IReadOnlyList<Resource> Resources => _resources.AsReadOnly();
    public IReadOnlyList<Report> Reports => _reports.AsReadOnly();
    public IReadOnlyList<LessonPlan> LessonPlans => _lessonPlans.AsReadOnly();
    public IReadOnlyList<WeekPlanner> WeekPlanners => _weekPlanners.AsReadOnly();
    public IReadOnlyList<YearData> YearDataHistory => _yearDataHistory.AsReadOnly();

    private Teacher(Guid id, Guid userId, string firstName, string lastName) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        UserId = userId;
    }

    public static Teacher Create(Guid userId, string firstName, string lastName)
    {
        return new(Guid.NewGuid(), userId, firstName, lastName);
    }

    public YearData? GetYearData(int year)
    {
        return _yearDataHistory.FirstOrDefault(s => s.CalendarYear == year);
    }

    public void AddYearData(YearData yearData)
    {
        if (!YearDataExists(yearData))
        {
            _yearDataHistory.Add(yearData);
        }
    }

    public void AddYearData(int year)
    {
        if (!YearDataExists(year))
        {
            _yearDataHistory.Add(YearData.Create(year));
        }
    }

    public void AddYearData(int year, List<Student> students)
    {
        if (!YearDataExists(year))
        {
            _yearDataHistory.Add(YearData.Create(year, students));
        }

    }

    private bool YearDataExists(YearData yearData)
    {
        return _yearDataHistory.Any(y => y.CalendarYear == yearData.CalendarYear);
    }

    private bool YearDataExists(int year)
    {
        return _yearDataHistory.Any(y => y.CalendarYear == year);
    }

    public void AddSubjectsTaught(List<Subject> subjects, int calendarYear)
    {
        var yearData = GetYearData(calendarYear);

        if (yearData is null)
        {
            yearData = YearData.Create(calendarYear);
            _yearDataHistory.Add(yearData);
        }

        yearData.AddSubjects(subjects);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Teacher() { }
}
