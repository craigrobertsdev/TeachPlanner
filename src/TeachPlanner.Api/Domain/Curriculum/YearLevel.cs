using TeachPlanner.Shared.Domain.Common.Enums;

namespace TeachPlanner.Api.Domain.CurriculumSubjects;

public record YearLevel {
    private readonly List<Strand> _strands = new();

    private YearLevel(
        List<Strand> strands,
        YearLevelValue? yearLevelValue,
        BandLevelValue? bandLevelValue,
        string? yearLevelDescription = null,
        string? achievementStandard = null
    ) {
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

    public string Name {
        get {
            if (YearLevelValue != null)
                return this.YearLevelValue == Shared.Domain.Common.Enums.YearLevelValue.Foundation
                    ? "Foundation"
                    : YearLevelValue.ToString()!.Insert(4, " ");
            // Convert Years1And2 to "Years 1 and 2"
            return BandLevelValue == Shared.Domain.Common.Enums.BandLevelValue.Foundation
                ? "Foundation"
                : BandLevelValue.ToString()!.Insert(5, " ").Insert(7, " ").Insert(10, " ");
        }
    }

    public static YearLevel Create(List<Strand> strands, string yearLevelDescription, string achievementStandard,
        YearLevelValue? yearLevelValue, BandLevelValue? bandLevelValue) {
        return new YearLevel(strands, yearLevelValue, bandLevelValue, yearLevelDescription, achievementStandard);
    }

    public YearLevelValue[] GetYearLevelsFromBandLevel() {
        if (BandLevelValue == null) {
            return new[] { YearLevelValue!.Value };
        }

        return BandLevelValue switch {
            Shared.Domain.Common.Enums.BandLevelValue.Foundation => new[] { Shared.Domain.Common.Enums.YearLevelValue.Foundation },
            Shared.Domain.Common.Enums.BandLevelValue.Years1To2 => new[] { Shared.Domain.Common.Enums.YearLevelValue.Year1, Shared.Domain.Common.Enums.YearLevelValue.Year2 },
            Shared.Domain.Common.Enums.BandLevelValue.Years3To4 => new[] { Shared.Domain.Common.Enums.YearLevelValue.Year3, Shared.Domain.Common.Enums.YearLevelValue.Year4 },
            Shared.Domain.Common.Enums.BandLevelValue.Years5To6 => new[] { Shared.Domain.Common.Enums.YearLevelValue.Year5, Shared.Domain.Common.Enums.YearLevelValue.Year6 },
            _ => throw new ArgumentOutOfRangeException()
        };
    }


    public Strand? GetStrand(Strand strand) {
        return _strands.Find(s => s.Name == strand.Name);
    }

    public void AddStrand(Strand strand) {
        _strands.Add(strand);
    }

    public List<ContentDescription> GetContentDescriptions() {
        var contentDescriptions = Strands.SelectMany(s => s.ContentDescriptions).ToList();
        return contentDescriptions;
    }

    public List<YearLevelValue> GetYearLevels() {
        var yearLevels = new List<YearLevelValue>();

        if (YearLevelValue is YearLevelValue v) {
            yearLevels.Add(v);
            return yearLevels;
        }

        switch (BandLevelValue) {
            case Shared.Domain.Common.Enums.BandLevelValue.Foundation:
                yearLevels.Add(Shared.Domain.Common.Enums.YearLevelValue.Foundation);
                break;
            case Shared.Domain.Common.Enums.BandLevelValue.Years1To2:
                yearLevels.Add(Shared.Domain.Common.Enums.YearLevelValue.Year1);
                yearLevels.Add(Shared.Domain.Common.Enums.YearLevelValue.Year2);
                break;
            case Shared.Domain.Common.Enums.BandLevelValue.Years3To4:
                yearLevels.Add(Shared.Domain.Common.Enums.YearLevelValue.Year3);
                yearLevels.Add(Shared.Domain.Common.Enums.YearLevelValue.Year4);
                break;
            case Shared.Domain.Common.Enums.BandLevelValue.Years5To6:
                yearLevels.Add(Shared.Domain.Common.Enums.YearLevelValue.Year5);
                yearLevels.Add(Shared.Domain.Common.Enums.YearLevelValue.Year6);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return yearLevels;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private YearLevel() {
    }
}