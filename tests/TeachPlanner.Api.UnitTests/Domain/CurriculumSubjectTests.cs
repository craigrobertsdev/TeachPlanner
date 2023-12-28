using FluentAssertions;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.UnitTests.Helpers;

namespace TeachPlanner.Api.UnitTests.Domain;
public class CurriculumSubjectTests {

    [Fact]
    public void RemoveYearLevelsNotTaught_WhenYearLevelHasUntaughtSubject_ShouldRemoveUntaughtYearLevels() {
        // Arrange
        var subject = SubjectHelpers.CreateCurriculumSubjectWithYearLevels();
        var yearLevels = new List<YearLevelValue> { YearLevelValue.Year1, YearLevelValue.Year2 };

        // Act
        var redactedYearLevels = subject.RemoveYearLevelsNotTaught(yearLevels);

        // Assert
        redactedYearLevels.Should().HaveCount(1);
        redactedYearLevels.Should().Contain(yl => yl.YearLevelValue == YearLevelValue.Year1);
    }

    [Fact]
    public void RemoveYearLevelsNotTaught_WhenYearLevelHasBandLevels_ShouldRetunMatchingYearLevel() {
        // Arrange
        var subject = SubjectHelpers.CreateCurriculumSubjectWithBandLevels();
        var yearLevels = new List<YearLevelValue> { YearLevelValue.Year1 };
        
        // Act
        var redactedYearLevels = subject.RemoveYearLevelsNotTaught(yearLevels);

        // Assert
        redactedYearLevels.Should().HaveCount(1);
        redactedYearLevels.Should().Contain(yl => yl.BandLevelValue == BandLevelValue.Years1To2);
    }
}
