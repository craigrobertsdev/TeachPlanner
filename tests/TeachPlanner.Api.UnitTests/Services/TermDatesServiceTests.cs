using FluentAssertions;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Services;

namespace TeachPlanner.Api.UnitTests.Services;

public class TermDatesServiceTests
{
    private readonly TermDatesService _termDatesService = new();

    public TermDatesServiceTests()
    {
        _termDatesService.SetTermDates(new List<TermDate>
        {
            new(1, new DateOnly(2023, 1, 30), new DateOnly(2023, 4, 14)),
            new(2, new DateOnly(2023, 5, 1), new DateOnly(2023, 7, 7)),
            new(3, new DateOnly(2023, 7, 24), new DateOnly(2023, 9, 29)),
            new(4, new DateOnly(2023, 10, 16), new DateOnly(2023, 12, 10))
        });
    }

    [Fact]
    public void GetWeekStart_WhenPassedValidArguments_ReturnsCorrectDate()
    {
        // Arrange
        var termNumber = 2;
        var weekNumber = 5;

        // Act
        var result = _termDatesService.GetWeekStart(termNumber, weekNumber);

        // Assert
        result.Should().Be(new DateOnly(2023, 5, 29));
    }

    [Fact]
    public void WeekStart_WhenPassedInvalidTermNumber_ThrowsArgumentException()
    {
        // Arrange
        var termNumber = 5;
        var weekNumber = 5;

        // Act
        Action act = () => _termDatesService.GetWeekStart(termNumber, weekNumber);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void WeekStart_WhenPassedInvalidWeekNumber_ThrowsArgumentException()
    {
        // Arrange
        var termNumber = 2;
        var weekNumber = -1;

        // Act
        Action act = () => _termDatesService.GetWeekStart(termNumber, weekNumber);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}
