using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Exceptions;
using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.Students;
public class StudentsForYear : ValueObject
{
    private readonly List<Student> _students = new();
    private readonly List<YearLevelValue> _yearLevelsTaught = new();
    public int CalendarYear { get; private set; }
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public IReadOnlyList<YearLevelValue> YearLevelsTaught => _yearLevelsTaught.AsReadOnly();

    private StudentsForYear(int calendarYear)
    {
        CalendarYear = calendarYear;
    }

    public static StudentsForYear Create(int calendarYear)
    {
        return new(calendarYear);
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
    private StudentsForYear() { }
}
