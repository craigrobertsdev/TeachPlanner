using Domain.Common.Enums;
using Domain.Common.Primatives;

namespace Domain.SubjectAggregates;

public sealed class YearLevel : ValueObject
{
    private readonly List<Strand> _strands = new();
    public string Name => YearLevelValue == YearLevelValue.Foundation ? "Foundation" : YearLevelValue.ToString().Insert(4, " ");
    public YearLevelValue YearLevelValue { get; private set; }
    public string? YearLevelDescription { get; private set; }
    public string? AchievementStandard { get; private set; }
    public IReadOnlyList<Strand> Strands => _strands.AsReadOnly();

    private YearLevel(
        YearLevelValue yearLevelValue,
        List<Strand> strands,
        string? yearLevelDescription = null,
        string? achievementStandard = null
    )
    {
        YearLevelValue = yearLevelValue;
        _strands = strands;
        YearLevelDescription = yearLevelDescription;
        AchievementStandard = achievementStandard;
    }

    public static YearLevel Create(YearLevelValue yearLevelValue, List<Strand> strands, string yearLevelDescription, string achievementStandard)
    {
        return new(yearLevelValue, strands, yearLevelDescription, achievementStandard);
    }

    public void AddStrand(Strand strand)
    {
        _strands.Add(strand);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return YearLevelValue;
        yield return Strands;
        yield return YearLevelDescription;
        yield return AchievementStandard;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearLevel() { }
}
