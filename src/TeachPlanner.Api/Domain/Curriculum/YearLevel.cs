using TeachPlanner.Api.Domain.Common.Enums;

namespace TeachPlanner.Api.Domain.CurriculumSubjects;

public record YearLevel
{
    private readonly List<Strand> _strands = new();

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

    public YearLevelValue? YearLevelValue { get; }
    public BandLevelValue? BandLevelValue { get; }
    public string? YearLevelDescription { get; private set; }
    public string? AchievementStandard { get; private set; }
    public IReadOnlyList<Strand> Strands => _strands.AsReadOnly();

    public string Name
    {
        get
        {
            if (YearLevelValue != null)
                return YearLevelValue == Common.Enums.YearLevelValue.Foundation
                    ? "Foundation"
                    : YearLevelValue.ToString()!.Insert(4, " ");
            // Convert Years1And2 to "Years 1 and 2"
            return BandLevelValue == Common.Enums.BandLevelValue.Foundation
                ? "Foundation"
                : BandLevelValue.ToString()!.Insert(5, " ").Insert(7, " ").Insert(10, " ");
        }
    }

    public static YearLevel Create(List<Strand> strands, string yearLevelDescription, string achievementStandard,
        YearLevelValue? yearLevelValue, BandLevelValue? bandLevelValue)
    {
        return new YearLevel(strands, yearLevelValue, bandLevelValue, yearLevelDescription, achievementStandard);
    }

    public Strand? GetStrand(Strand strand)
    {
        return _strands.Find(s => s.Name == strand.Name);
    }

    public void AddStrand(Strand strand)
    {
        _strands.Add(strand);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearLevel()
    {
    }
}