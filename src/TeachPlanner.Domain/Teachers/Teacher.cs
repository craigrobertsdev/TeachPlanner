using TeachPlanner.Domain.Assessments;
using TeachPlanner.Domain.Common.Primatives;
using TeachPlanner.Domain.Resources;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Domain.Teachers;

public sealed class Teacher : AggregateRoot
{
    private readonly List<Guid> _subjectsTaughtIds = new();
    private readonly List<SummativeAssessment> _summativeAssessments = new();
    private readonly List<FormativeAssessment> _formativeAssessments = new();
    private readonly List<Resource> _resources = new();
    private readonly Dictionary<int, YearDataId> _yearDataHistory = new();
    public Guid UserId { get; private set; } = Guid.Empty;
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public IReadOnlyList<Guid> SubjectsTaughtIds => _subjectsTaughtIds.AsReadOnly();
    public IReadOnlyList<SummativeAssessment> SummativeAssessments => _summativeAssessments.AsReadOnly();
    public IReadOnlyList<FormativeAssessment> FormativeAssessments => _formativeAssessments.AsReadOnly();
    public IReadOnlyList<Resource> Resources => _resources.AsReadOnly();
    public IReadOnlyDictionary<int, YearDataId> YearDataHistory => _yearDataHistory.AsReadOnly();

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

    public YearDataId? GetYearData(int year)
    {
        return _yearDataHistory.GetValueOrDefault(year);
    }

    public void AddYearData(YearData yearData)
    {
        if (!YearDataExists(yearData))
        {
            _yearDataHistory.Add(yearData.CalendarYear, yearData.Id);
        }
    }

    private bool YearDataExists(YearData yearData)
    {
        return YearDataExists(yearData.CalendarYear);
    }

    private bool YearDataExists(int year)
    {
        return _yearDataHistory.ContainsKey(year);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Teacher() { }
}
