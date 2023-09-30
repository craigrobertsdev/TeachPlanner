using TeachPlanner.Api.Entities.Assessments;
using TeachPlanner.Api.Entities.Common.Interfaces;
using TeachPlanner.Api.Entities.Common.Primatives;
using TeachPlanner.Api.Entities.Resources;
using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.YearDataRecords;

namespace TeachPlanner.Api.Entities.Teachers;

public sealed class Teacher : Entity<TeacherId>, IAggregateRoot
{
    private readonly List<SubjectId> _subjectsTaughtIds = new();
    private readonly List<SummativeAssessment> _summativeAssessments = new();
    private readonly List<FormativeAssessment> _formativeAssessments = new();
    private readonly List<Resource> _resources = new();
    private readonly List<YearDataEntry> _yearDataHistory = new();
    public Guid UserId { get; private set; } = Guid.Empty;
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public IReadOnlyList<SubjectId> SubjectsTaughtIds => _subjectsTaughtIds.AsReadOnly();
    public IReadOnlyList<SummativeAssessment> SummativeAssessments => _summativeAssessments.AsReadOnly();
    public IReadOnlyList<FormativeAssessment> FormativeAssessments => _formativeAssessments.AsReadOnly();
    public IReadOnlyList<Resource> Resources => _resources.AsReadOnly();
    public IReadOnlyList<YearDataEntry> YearDataHistory => _yearDataHistory.AsReadOnly();

    private Teacher(TeacherId id, Guid userId, string firstName, string lastName) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        UserId = userId;
    }

    public static Teacher Create(Guid userId, string firstName, string lastName)
    {
        return new(new TeacherId(Guid.NewGuid()), userId, firstName, lastName);
    }

    public YearDataId? GetYearData(int year)
    {
        var yearDataEntry = _yearDataHistory.FirstOrDefault(yd => yd.CalendarYear == year);

        if (yearDataEntry is null)
        {
            return null;
        }

        return yearDataEntry.YearDataId;
    }

    public void AddYearData(YearData yearData)
    {
        if (!YearDataExists(yearData))
        {
            _yearDataHistory.Add(new YearDataEntry(yearData.CalendarYear, yearData.Id));
        }
    }

    private bool YearDataExists(YearData yearData)
    {
        return YearDataExists(yearData.CalendarYear);
    }

    private bool YearDataExists(int year)
    {
        return _yearDataHistory.FirstOrDefault(yd => yd.CalendarYear == year) is not null;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Teacher() { }
}
