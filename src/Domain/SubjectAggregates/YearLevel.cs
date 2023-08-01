using Domain.Common.Enums;
using Domain.Common.Primatives;

namespace Domain.SubjectAggregates;

public sealed class YearLevel : ValueObject
{
    private readonly List<Strand> _strands = new();
    public string Name { get; private set; }
    public YearLevelValue YearLevelValue { get; private set; }
    public string? YearLevelDescription { get; private set; }
    public string? AchievementStandard { get; private set; }
    public IReadOnlyList<Strand> Strands => _strands.AsReadOnly();

    private YearLevel(
        string name,
        YearLevelValue yearLevelValue,
        List<Strand> strands,
        string? yearLevelDescription = null,
        string? achievementStandard = null
    )
    {
        Name = name;
        YearLevelValue = yearLevelValue;
        _strands = strands;
        YearLevelDescription = yearLevelDescription;
        AchievementStandard = achievementStandard;
    }

    public static YearLevel Create(string name, YearLevelValue yearLevelValue, List<Strand> strands)
    {
        return new(name, yearLevelValue, strands);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return YearLevelValue;
        yield return Strands;
        yield return YearLevelDescription;
        yield return AchievementStandard;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearLevel() { }
}
