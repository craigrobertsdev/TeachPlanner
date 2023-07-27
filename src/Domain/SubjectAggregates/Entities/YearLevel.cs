using Domain.Common.Enums;
using Domain.Common.Primatives;
using Domain.SubjectAggregates.ValueObjects;

namespace Domain.SubjectAggregates.Entities;

public sealed class YearLevel : Entity<YearLevelId>
{
    private readonly List<Strand> _strands = new();
    public string Name { get; private set; }
    public YearLevelValue YearLevelValue { get; private set; }
    public string? YearLevelDescription { get; private set; }
    public string? AchievementStandard { get; private set; }
    public IReadOnlyList<Strand> Strands => _strands.AsReadOnly();

    private YearLevel(
        YearLevelId id,
        string name,
        YearLevelValue yearLevelValue,
        List<Strand> strands,
        string? yearLevelDescription = null,
        string? achievementStandard = null
    )
        : base(id)
    {
        Name = name;
        YearLevelValue = yearLevelValue;
        _strands = strands;
        YearLevelDescription = yearLevelDescription;
        AchievementStandard = achievementStandard;
    }

    public static YearLevel Create(string name, YearLevelValue yearLevelValue, List<Strand> strands)
    {
        return new(YearLevelId.Create(), name, yearLevelValue, strands);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearLevel() { }
}
