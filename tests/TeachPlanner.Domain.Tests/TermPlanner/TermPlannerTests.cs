using FluentAssertions;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Exceptions;
using TeachPlanner.Domain.TermPlanners;
namespace TeachPlanner.Domain.Tests.TermPlanners;
public class TermPlannerTests
{
    [Fact]
    public void Create_OnValidInput_ShouldReturnTermPlanner()
    {
        // Arrange

        // Act
        var result = TermPlanner.Create(2021, new List<TermPlan>(), YearLevelValue.Year1);

        // Assert
        result.Should().BeOfType<TermPlanner>();
        result.CalendarYear.Should().Be(2021);
        result.YearLevel.Should().Be(YearLevelValue.Year1);
        result.YearLevels.Should().BeEmpty();
    }

    [Fact]
    public void AddTermPlanner_OnAddingTermPlan_ShouldBeAdded()
    {
        // Arrange
        var termPlanner = TermPlanner.Create(2021, new List<TermPlan>(), YearLevelValue.Year1);
        var termPlan = TermPlan.Create(new List<string> { "ENG" });

        // Act
        var result = termPlanner.AddTermPlan(termPlan);

        // Assert
        termPlanner.TermPlans.Should().HaveCount(1);
        termPlanner.TermPlans.Should().Contain(termPlan);
        result.Should().Be(true);
    }

    [Fact]
    public void AddTermPlanner_OnAddingDuplicateTermPlan_ShouldNotBeAdded()
    {
        // Arrange
        var termPlanner = TermPlanner.Create(2021, new List<TermPlan>(), YearLevelValue.Year1);
        var termPlan = TermPlan.Create(new List<string> { "ENG" });

        // Act
        var result1 = termPlanner.AddTermPlan(termPlan);
        var result2 = termPlanner.AddTermPlan(termPlan);

        // Assert
        termPlanner.TermPlans.Should().HaveCount(1);
        termPlanner.TermPlans.Should().Contain(termPlan);
        result1.Should().Be(true);
        result2.Should().Be(false);
    }

    [Fact]
    public void Create_OnPassingYearLevelAndYearLevelList_ShouldThrowException()
    {
        // Arrange

        // Act
        Action act = () => TermPlanner.Create(2021, new List<TermPlan>(), YearLevelValue.Year1, new List<YearLevelValue> { YearLevelValue.Year1 });

        // Assert
        act.Should().Throw<TermPlannerCreationException>();
    }

    [Fact]
    public void Create_OnPassingNeitherYearLevelAndYearLevelList_ShouldThrowException()
    {
        // Arrange

        // Act
        Action act = () => TermPlanner.Create(2021, new List<TermPlan>(), null, null);

        // Assert
        act.Should().Throw<TermPlannerCreationException>();
    }
}
