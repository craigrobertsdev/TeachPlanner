using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Primatives;

namespace TeachPlanner.Api.Domain.Subjects;

public sealed class Subject : Entity<SubjectId>, IAggregateRoot
{
    private readonly List<YearLevel> _yearLevels = new();
    public string Name { get; private set; }
    public IReadOnlyList<YearLevel> YearLevels => _yearLevels.AsReadOnly();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }
    public bool IsCurriculumSubject { get; private set; }

    private Subject(
        SubjectId id,
        List<YearLevel> yearLevels,
        string name,
        bool isCurriculumSubject = false
    )
        : base(id)
    {
        _yearLevels = yearLevels;
        Name = name;
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
        IsCurriculumSubject = isCurriculumSubject;
    }

    public static Subject Create(
        string name,
        List<YearLevel> yearLevels)
    {
        return new Subject(
            new SubjectId(Guid.NewGuid()),
            yearLevels,
            name);
    }

    public static Subject CreateCurriculumSubject(
        string name,
        List<YearLevel> yearLevels)
    {
        return new Subject(
            new SubjectId(Guid.NewGuid()),
            yearLevels,
            name,
            true);
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

