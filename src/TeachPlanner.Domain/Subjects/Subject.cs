using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.Subjects;

public sealed class Subject : AggregateRoot
{
    private readonly bool _isCurriculumSubject = false;
    private readonly List<YearLevel> _yearLevels = new();
    public string Name { get; private set; }
    public IReadOnlyList<YearLevel> YearLevels => _yearLevels.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private Subject(
        Guid id,
        List<YearLevel> yearLevels,
        string name
    )
        : base(id)
    {
        _yearLevels = yearLevels;
        Name = name;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public static Subject Create(
        string name,
        List<YearLevel> yearLevels)
    {
        return new Subject(
            Guid.NewGuid(),
            yearLevels,
            name);
    }

    public YearLevel? GetYearLevel(YearLevel yearLevel)
    {
        return _yearLevels.Find(yl => yl.Name == yearLevel.Name);
    }

    public void AddYearLevel(YearLevel yearLevel)
    {
        _yearLevels.Add(yearLevel);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Subject() { }
}

