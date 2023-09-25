using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Exceptions;
using TeachPlanner.Domain.Common.Primatives;
using TeachPlanner.Domain.Students;
using TeachPlanner.Domain.Subjects;

namespace TeachPlanner.Domain.Teachers;
public class YearData : ValueObject
{
    private readonly List<Student> _students = new();
    private readonly List<Subject> _subjects = new();
    private readonly List<YearLevelValue> _yearLevelsTaught = new();
    public int CalendarYear { get; private set; }
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public IReadOnlyList<YearLevelValue> YearLevelsTaught => _yearLevelsTaught.AsReadOnly();
    public IReadOnlyList<Subject> Subjects => _subjects.AsReadOnly();

    private YearData(int calendarYear)
    {
        CalendarYear = calendarYear;
    }

    private YearData(int calendarYear, List<Student> students)
    {
        CalendarYear = calendarYear;
        _students = students;
    }

    public static YearData Create(int calendarYear)
    {
        return new(calendarYear);
    }

    public static YearData Create(int calendarYear, List<Student> students)
    {
        return new(calendarYear, students);
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

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return CalendarYear;
        yield return _yearLevelsTaught;
        yield return _students;
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
        if (_yearLevelsTaught.Count >= 2)
        {
            throw new TooManyYearLevelsException();
        }

        if (NotInYearLevelsTaught(yearLevel))
        {
            _yearLevelsTaught.Add(yearLevel);
        }
    }

    private bool NotInYearLevelsTaught(YearLevelValue yearLevel)
    {
        return _yearLevelsTaught.Contains(yearLevel);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearData() { }
}
