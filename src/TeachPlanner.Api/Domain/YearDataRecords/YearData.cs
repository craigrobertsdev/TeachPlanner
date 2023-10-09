using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Primatives;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.Reports;
using TeachPlanner.Api.Domain.Students;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.WeekPlanners;
using TeachPlanner.Api.Domain.YearDataRecords.DomainEvents;

namespace TeachPlanner.Api.Domain.YearDataRecords;
public class YearData : Entity<YearDataId>, IAggregateRoot
{
    private readonly List<Student> _students = new();
    private readonly List<Subject> _subjects = new();
    private readonly List<YearLevelValue> _yearLevelsTaught = new();
    private readonly List<LessonPlan> _lessonPlans = new();
    private readonly List<WeekPlanner> _weekPlanners = new();
    public TeacherId TeacherId { get; private set; }
    public TermPlannerId? TermPlannerId { get; private set; }
    public int CalendarYear { get; private set; }
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public IReadOnlyList<YearLevelValue> YearLevelsTaught => _yearLevelsTaught.AsReadOnly();
    public IReadOnlyList<Subject> Subjects => _subjects.AsReadOnly();
    public IReadOnlyList<LessonPlan> LessonPlans => _lessonPlans.AsReadOnly();
    public IReadOnlyList<WeekPlanner> WeekPlanners => _weekPlanners.AsReadOnly();

    private YearData(YearDataId id, TeacherId teacherId, int calendarYear) : base(id)
    {
        TeacherId = teacherId;
        CalendarYear = calendarYear;
    }

    private YearData(YearDataId id, TeacherId teacherId, int calendarYear, List<Student> students) : base(id)
    {
        TeacherId = teacherId;
        CalendarYear = calendarYear;
        _students = students;
    }

    public static YearData Create(TeacherId teacherId, int calendarYear)
    {
        var yearData = new YearData(new YearDataId(Guid.NewGuid()), teacherId, calendarYear);

        yearData.AddDomainEvent(new YearDataCreatedDomainEvent(Guid.NewGuid(), yearData.Id, calendarYear, teacherId));

        return yearData;
    }

    public static YearData Create(TeacherId teacherId, int calendarYear, List<Student> students)
    {
        var yearData = new YearData(new YearDataId(Guid.NewGuid()), teacherId, calendarYear, students);

        yearData.AddDomainEvent(new YearDataCreatedDomainEvent(Guid.NewGuid(), yearData.Id, calendarYear, teacherId));

        return yearData;
    }

    public void AddSubjects(List<CurriculumSubject> subjects)
    {
        foreach (var subject in subjects)
        {
            if (IsInSubjects(subject))
            {
                return;
            }

            _subjects.Add(Subject.Create(subject.Name, new List<YearDataContentDescription>()));
        }
    }

    private bool IsInSubjects(CurriculumSubject subject)
    {
        return _subjects.FirstOrDefault(s => s.Name == subject.Name) != null;
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
        return !_students.Contains(student);
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

    public void AddTermPlanner(TermPlannerId termPlannerId)
    {
        if (TermPlannerId is not null)
        {
            throw new TermPlannerAlreadyAssociatedException();
        }

        TermPlannerId = termPlannerId;
    }

    public void AddYearLevelsTaught(List<YearLevelValue> yearLevelsTaught)
    {
        foreach (var yearLevel in yearLevelsTaught)
        {
            AddYearLevel(yearLevel);
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearData() { }
}
