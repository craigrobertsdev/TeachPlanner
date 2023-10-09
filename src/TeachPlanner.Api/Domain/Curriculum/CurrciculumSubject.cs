using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Primatives;

namespace TeachPlanner.Api.Domain.CurriculumSubjects;

public sealed class CurriculumSubject : Entity<CurriculumSubjectId>, IAggregateRoot
{
    private readonly List<YearLevel> _yearLevels = new();
    public string Name { get; private set; }
    public IReadOnlyList<YearLevel> YearLevels => _yearLevels.AsReadOnly();

    private CurriculumSubject(
        CurriculumSubjectId id,
        List<YearLevel> yearLevels,
        string name
    )
        : base(id)
    {
        _yearLevels = yearLevels;
        Name = name;
    }

    public static CurriculumSubject Create(
        string name,
        List<YearLevel> yearLevels)
    {
        return new CurriculumSubject(
            new CurriculumSubjectId(Guid.NewGuid()),
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
    private CurriculumSubject() { }
}

