using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Primatives;

namespace TeachPlanner.Domain.Subjects;

public sealed class YearLevel : ValueObject
{
    private readonly List<Strand> _strands = new();
    public YearLevelValue? YearLevelValue { get; private set; }
    public BandLevelValue? BandLevelValue { get; private set; }
    public string? YearLevelDescription { get; private set; }
    public string? AchievementStandard { get; private set; }
    public IReadOnlyList<Strand> Strands => _strands.AsReadOnly();

    public string Name
    {
        get
        {
            if (YearLevelValue != null)
            {

                return YearLevelValue == Common.Enums.YearLevelValue.Foundation
                    ? "Foundation"
                    : YearLevelValue.ToString()!.Insert(4, " ");
            }
            else
            {
                // Convert Years1And2 to "Years 1 and 2"
                return BandLevelValue == Common.Enums.BandLevelValue.Foundation
                    ? "Foundation"
                    : BandLevelValue.ToString()!.Insert(5, " ").Insert(7, " ").Insert(10, " ");
            }

        }
    }

    private YearLevel(
        List<Strand> strands,
        YearLevelValue? yearLevelValue,
        BandLevelValue? bandLevelValue,
        string? yearLevelDescription = null,
        string? achievementStandard = null
    )
    {
        _strands = strands;
        YearLevelValue = yearLevelValue;
        BandLevelValue = bandLevelValue;
        YearLevelDescription = yearLevelDescription;
        AchievementStandard = achievementStandard;
    }

    public static YearLevel Create(List<Strand> strands, string yearLevelDescription, string achievementStandard, YearLevelValue? yearLevelValue, BandLevelValue? bandLevelValue)
    {
        return new(strands, yearLevelValue, bandLevelValue, yearLevelDescription, achievementStandard);
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
