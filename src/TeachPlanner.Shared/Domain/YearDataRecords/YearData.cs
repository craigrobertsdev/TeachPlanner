using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Domain.YearDataRecords.DomainEvents;
using TeachPlanner.Shared.Domain.Common.Enums;
using TeachPlanner.Shared.Domain.TermPlanners;
using TeachPlanner.Shared.Domain.Common.Interfaces;
using TeachPlanner.Shared.Domain.Common.Primatives;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.Students;
using TeachPlanner.Shared.Domain.WeekPlanners;
using TeachPlanner.Shared.Domain.LessonPlans;
using TeachPlanner.Shared.Domain.Curriculum;
using TeachPlanner.Shared.Domain.PlannerTemplates;

namespace TeachPlanner.Shared.Domain.YearDataRecords;

public class YearData : Entity<YearDataId>, IAggregateRoot {
    private readonly List<LessonPlan> _lessonPlans = new();
    private readonly List<Student> _students = new();
    private readonly List<Subject> _subjects = new();
    private readonly List<WeekPlanner> _weekPlanners = new();
    private readonly List<YearLevelValue> _yearLevelsTaught = new();
    public DayPlanTemplate DayPlanTemplate { get; private set; }

    public TeacherId TeacherId { get; private set; }
    public TermPlannerId? TermPlannerId { get; private set; }
    public int CalendarYear { get; private set; }
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public IReadOnlyList<YearLevelValue> YearLevelsTaught => _yearLevelsTaught.AsReadOnly();
    public IReadOnlyList<Subject> Subjects => _subjects.AsReadOnly();
    public IReadOnlyList<LessonPlan> LessonPlans => _lessonPlans.AsReadOnly();
    public IReadOnlyList<WeekPlanner> WeekPlanners => _weekPlanners.AsReadOnly();

    private YearData(YearDataId id, TeacherId teacherId, DayPlanTemplate dayPlanTemplate, int calendarYear) : base(id) {
        TeacherId = teacherId;
        CalendarYear = calendarYear;
        DayPlanTemplate = dayPlanTemplate;
    }

    private YearData(YearDataId id, TeacherId teacherId, DayPlanTemplate dayPlanTemplate, int calendarYear, List<Student> students) : base(id) {
        TeacherId = teacherId;
        CalendarYear = calendarYear;
        DayPlanTemplate = dayPlanTemplate;
        _students = students;
    }

    private YearData(YearDataId id, TeacherId teacherId, DayPlanTemplate dayPlanTemplate, int calendarYear, List<string> yearLevels) : base(id) {
        TeacherId = teacherId;
        CalendarYear = calendarYear;
        DayPlanTemplate = dayPlanTemplate;
        var yearLevelEnums = yearLevels.Select(Enum.Parse<YearLevelValue>).ToList();
        yearLevelEnums.Sort();
        _yearLevelsTaught = yearLevelEnums;
    }

    public static YearData Create(TeacherId teacherId, int calendarYear, DayPlanTemplate dayPlanTemplate) {
        var yearData = new YearData(new YearDataId(Guid.NewGuid()), teacherId, dayPlanTemplate, calendarYear);

        yearData.AddDomainEvent(new YearDataCreatedDomainEvent(Guid.NewGuid(), yearData.Id, calendarYear, teacherId));

        return yearData;
    }

    public static YearData Create(TeacherId teacherId, int calendarYear, DayPlanTemplate dayPlanTemplate, List<string> yearLevels) {
        var yearData = new YearData(new YearDataId(Guid.NewGuid()), teacherId, dayPlanTemplate, calendarYear, yearLevels);

        yearData.AddDomainEvent(new YearDataCreatedDomainEvent(Guid.NewGuid(), yearData.Id, calendarYear, teacherId));

        return yearData;
    }

    public void AddSubjects(List<CurriculumSubject> subjects) {
        foreach (var subject in subjects) {
            if (IsInSubjects(subject)) return;

            _subjects.Add(Subject.Create(subject.Name, new List<YearDataContentDescription>()));
        }
    }

    private bool IsInSubjects(CurriculumSubject subject) {
        return _subjects.FirstOrDefault(s => s.Name == subject.Name) != null;
    }

    public void AddStudents(List<Student> students) {
        foreach (var student in students) AddStudent(student);
    }

    public void AddStudent(Student student) {
        if (NotInStudents(student)) _students.Add(student);
    }

    private bool NotInStudents(Student student) {
        return !_students.Contains(student);
    }

    public void AddYearLevel(YearLevelValue yearLevel) {
        if (NotInYearLevelsTaught(yearLevel)) _yearLevelsTaught.Add(yearLevel);
    }

    private bool NotInYearLevelsTaught(YearLevelValue yearLevel) {
        return _yearLevelsTaught.Contains(yearLevel);
    }

    public void AddTermPlanner(TermPlannerId termPlannerId) {
        if (TermPlannerId is not null) throw new TermPlannerAlreadyAssociatedException();

        TermPlannerId = termPlannerId;
    }

    public void AddYearLevelsTaught(List<YearLevelValue> yearLevelsTaught) {
        foreach (var yearLevel in yearLevelsTaught) AddYearLevel(yearLevel);
    }

    public void SetDayPlanTemplate(DayPlanTemplate dayPlanTemplate) {
        DayPlanTemplate.SetPeriods(dayPlanTemplate.Periods);
        _domainEvents.Add(new DayPlanTemplateAddedToYearDataEvent(Guid.NewGuid(), DayPlanTemplate.Id));
    }

    public void SetYearLevelsTaught(List<YearLevelValue> yearLevels) {
        yearLevels.Sort();
        _yearLevelsTaught.Clear();
        _yearLevelsTaught.AddRange(yearLevels);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearData() {
    }
}