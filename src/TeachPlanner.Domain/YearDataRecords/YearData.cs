using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Exceptions;
using TeachPlanner.Domain.Common.Primatives;
using TeachPlanner.Domain.Students;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.TermPlanners;

namespace TeachPlanner.Domain.YearDataRecords;
public class YearData : AggregateRoot
{
    private readonly List<Student> _students = new();
    private readonly List<Subject> _subjects = new();
    private readonly List<YearLevelValue> _yearLevelsTaught = new();
    public Guid TeacherId { get; private set; }
    public TermPlanner? TermPlanner { get; private set; }
    public int CalendarYear { get; private set; }
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public IReadOnlyList<YearLevelValue> YearLevelsTaught => _yearLevelsTaught.AsReadOnly();
    public IReadOnlyList<Subject> Subjects => _subjects.AsReadOnly();

    private YearData(Guid id, int calendarYear) : base(id)
    {
        CalendarYear = calendarYear;
    }

    private YearData(Guid id, int calendarYear, List<Student> students) : base(id)
    {
        CalendarYear = calendarYear;
        _students = students;
    }

    public static YearData Create(int calendarYear)
    {
        var yearData = new YearData(Guid.NewGuid(), calendarYear);

        yearData.Raise(new YearDataCreatedDomainEvent(Guid.NewGuid(), yearData.Id));

        return yearData;
    }

    public static YearData Create(int calendarYear, List<Student> students)
    {
        var yearData = new YearData(Guid.NewGuid(), calendarYear, students);

        yearData.Raise(new YearDataCreatedDomainEvent(Guid.NewGuid(), yearData.Id));

        return yearData;
    }

    public void AddSubjects(List<Subject> subjects)
    {
        CheckForNonCurriculumSubjects(subjects);

        foreach (var subject in subjects)
        {
            if (NotInSubjects(subject))
            {
                _subjects.Add(subject);
            }
        }
    }

    private static void CheckForNonCurriculumSubjects(List<Subject> subjects)
    {
        foreach (var subject in subjects)
        {
            if (subject.IsCurriculumSubject == false)
            {
                throw new IsNonCurriculumSubjectException(subject);
            }
        }
    }

    private bool NotInSubjects(Subject subject)
    {
        return !_subjects.Contains(subject);
    }

    public void AddStudents(List<Student> students)
    {
        foreach (var student in students)
        {
            AddStudent(student);
        }
    }

    public void AddStudent(Student student)
    {
        if (NotInStudents(student))
        {
            _students.Add(student);
        }
    }

    private bool NotInStudents(Student student)
    {
        return _students.Contains(student);
    }

    public void AddYearLevel(YearLevelValue yearLevel)
    {
        if (NotInYearLevelsTaught(yearLevel))
        {
            _yearLevelsTaught.Add(yearLevel);
        }
    }

    private bool NotInYearLevelsTaught(YearLevelValue yearLevel)
    {
        return _yearLevelsTaught.Contains(yearLevel);
    }

    public void AddTermPlanner(TermPlanner termPlanner)
    {
        if (TermPlanner is not null)
        {
            throw new TermPlannerAlreadyExistsException();
        }

        TermPlanner = termPlanner;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearData() { }
}
