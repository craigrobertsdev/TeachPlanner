using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Domain.Common.Interfaces;
using TeachPlanner.Api.Domain.Common.Primatives;

namespace TeachPlanner.Api.Domain.CurriculumSubjects;

public sealed class CurriculumSubject : Entity<SubjectId>, IAggregateRoot {
    private readonly List<YearLevel> _yearLevels = new();

    private CurriculumSubject(
        SubjectId id,
        List<YearLevel> yearLevels,
        string name
    )
        : base(id) {
        _yearLevels = yearLevels;
        Name = name;
    }

    public string Name { get; private set; }
    public IReadOnlyList<YearLevel> YearLevels => _yearLevels.AsReadOnly();

    public static CurriculumSubject Create(
        string name,
        List<YearLevel> yearLevels) {
        return new CurriculumSubject(
            new SubjectId(Guid.NewGuid()),
            yearLevels,
            name);
    }

    public YearLevel? GetYearLevel(YearLevel yearLevel) {
        return _yearLevels.Find(yl => yl.Name == yearLevel.Name);
    }

    public void AddYearLevel(YearLevel yearLevel) {
        _yearLevels.Add(yearLevel);
    }

    public List<YearLevel> RemoveYearLevelsNotTaught(List<YearLevelValue> yearLevels) {
        var redactedYearLevels = new List<YearLevel>();
        foreach (var yearLevel in _yearLevels) {
            if (yearLevel.YearLevelValue.HasValue && yearLevels.Contains(yearLevel.YearLevelValue.Value)) {
                redactedYearLevels.Add(yearLevel);
                continue;
            }

            if (yearLevel.YearLevelValue.HasValue) {
                continue;
            }

            var yearLevelsFromBandLevel = yearLevel.GetYearLevelsFromBandLevel(); 
            if (yearLevels.Contains(yearLevelsFromBandLevel[0])) {
                redactedYearLevels.Add(yearLevel);
            } else if (yearLevelsFromBandLevel.Length > 1 && yearLevels.Contains(yearLevel.GetYearLevelsFromBandLevel()[1])) {
                redactedYearLevels.Add(yearLevel);
            }
        }

        return redactedYearLevels;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private CurriculumSubject() {
    }
}