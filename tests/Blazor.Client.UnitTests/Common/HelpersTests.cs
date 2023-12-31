using FluentAssertions;
using TeachPlanner.Blazor.Client.Common;

namespace Blazor.Client.UnitTests.Common;
public class HelpersTests {
    [Fact]
    public void GetTimeFromString_ShouldReturnTimeOnly() {
        // Arrange
        var time = "12:00:00";

        // Act
        var result = Helpers.GetTimeFromString(time);

        // Assert
        result.Should().HaveHours(12);
        result.Should().HaveMinutes(0);
        result.Should().HaveSeconds(0);
    }

    [Fact]
    public void GetTimeFromDate_ShouldReturnTimeOnly() {
        // Arrange
        var date = new DateTime(2021, 1, 1, 12, 0, 0);

        // Act
        var result = Helpers.GetTimeFromDate(date);

        // Assert
        result.Should().HaveHours(12);
        result.Should().HaveMinutes(0);
        result.Should().HaveSeconds(0);
    }
}
