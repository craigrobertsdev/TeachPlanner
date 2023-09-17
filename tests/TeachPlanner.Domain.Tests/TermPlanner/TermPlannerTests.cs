﻿using FluentAssertions;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Common.Exceptions;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.TermPlanners;
using TeachPlanner.Domain.Tests.Helpers;

namespace TeachPlanner.Domain.Tests.TermPlanners;
public class TermPlannerTests
{
    [Fact]
    public void Create_OnValidInput_ShouldReturnTermPlanner()
    {
        // Arrange

        // Act
        var termPlanner = TermPlanner.Create(2021, YearLevelValue.Year1, null);

        // Assert
        termPlanner.Should().BeOfType<TermPlanner>();
        termPlanner.CalendarYear.Should().Be(2021);
        termPlanner.YearLevels.Count.Should().Be(1);
        termPlanner.YearLevels[0].Should().Be(YearLevelValue.Year1);
    }


    [Fact]
    public void Create_OnAddingSameYearLevelTwice_ShouldThrowException()
    {
        // Arrange
        var termPlanner = TermPlanner.Create(2021, YearLevelValue.Year1, YearLevelValue.Year1);

        // Act

        // Assert
        termPlanner.YearLevels.Count.Should().Be(1);
        termPlanner.YearLevels[0].Should().Be(YearLevelValue.Year1);
    }

    [Fact]
    public void Create_OnCreating_YearLevelsShouldBeOrdered()
    {
        // Arrange
        var termPlanner = TermPlanner.Create(2021, YearLevelValue.Year5, YearLevelValue.Year1);

        // Act

        // Assert
        termPlanner.YearLevels[0].Should().Be(YearLevelValue.Year1);
        termPlanner.YearLevels[1].Should().Be(YearLevelValue.Year5);
    }

    [Fact]
    public void AddYearLevel_OnAddingYearLevel_ShouldBeAdded()
    {
        // Arrange
        var termPlanner = TermPlanner.Create(2021, YearLevelValue.Year1, null);

        // Act
        termPlanner.AddYearLevel(YearLevelValue.Year2);

        // Assert
        termPlanner.YearLevels.Should().HaveCount(2);
        termPlanner.YearLevels.Should().Contain(YearLevelValue.Year1);
        termPlanner.YearLevels.Should().Contain(YearLevelValue.Year2);
    }

    [Fact]
    public void AddYearLevel_OnAddingYearLevel_ShouldBeOrdered()
    {
        // Arrange
        var termPlanner = TermPlanner.Create(2021, YearLevelValue.Year5, null);

        // Act
        termPlanner.AddYearLevel(YearLevelValue.Year1);

        // Assert
        termPlanner.YearLevels[0].Should().Be(YearLevelValue.Year1);
        termPlanner.YearLevels[1].Should().Be(YearLevelValue.Year5);
    }
    [Fact]
    public void AddTermPlan_OnAddingTermPlan_ShouldBeAdded()
    {
        // Arrange
        var termPlanner = TermPlanner.Create(2021, YearLevelValue.Year1, null);
        var termPlan = TermPlan.Create(termPlanner, 1, new List<Subject> { TermPlannerHelpers.CreateSubject("English", "ENG001") });

        // Act
        termPlanner.AddTermPlan(termPlan);

        // Assert
        termPlanner.TermPlans.Should().HaveCount(1);
        termPlanner.TermPlans.Should().Contain(termPlan);
    }

    [Fact]
    public void AddTermPlan_OnAddingDuplicateTermPlan_ShouldNotBeAdded()
    {
        // Arrange
        var termPlanner = TermPlanner.Create(2021, YearLevelValue.Year1, null);
        var termPlan = TermPlan.Create(termPlanner, 1, new List<Subject> { TermPlannerHelpers.CreateSubject("English", "ENG001") });

        // Act
        termPlanner.AddTermPlan(termPlan);
        Action act = () => termPlanner.AddTermPlan(termPlan);

        // Assert
        termPlanner.TermPlans.Should().HaveCount(1);
        act.Should().Throw<DuplicateTermPlanException>();
    }

    [Fact]
    public void AddTermPlan_OnAddingFifthTermPlan_ShouldNotBeAdded()
    {
        // Arrange
        var termPlanner = TermPlannerHelpers.CreateTermPlanner();
        List<TermPlan> termPlans = new()
        {
            TermPlan.Create(termPlanner, 1, new List < Subject > { TermPlannerHelpers.CreateSubject("English", "ENG001") }),
            TermPlan.Create(termPlanner, 2, new List < Subject > { TermPlannerHelpers.CreateSubject("English", "ENG002") }),
            TermPlan.Create(termPlanner, 3, new List < Subject > { TermPlannerHelpers.CreateSubject("English", "ENG003") }),
            TermPlan.Create(termPlanner, 4, new List < Subject > { TermPlannerHelpers.CreateSubject("English", "ENG004") }),
        };

        foreach (var termPlan in termPlans)
        {
            termPlanner.AddTermPlan(termPlan);
        }

        // Act
        Action act = () => termPlanner.AddTermPlan(TermPlan.Create(termPlanner, 4, new List<Subject> { TermPlannerHelpers.CreateSubject("English", "ENG005") }));

        // Assert
        act.Should().Throw<TooManyTermPlansException>();
        termPlanner.TermPlans.Should().HaveCount(4);
    }

    [Fact]
    public void AddTermPlan_OnAddingDuplicateTermNumber_ShouldNotBeAdded()
    {
        // Arrange
        var termPlanner = TermPlannerHelpers.CreateTermPlanner();
        var termPlan = TermPlan.Create(termPlanner, 1, new List<Subject> { TermPlannerHelpers.CreateSubject("English", "ENG001") });

        // Act
        termPlanner.AddTermPlan(termPlan);
        Action act = () => termPlanner.AddTermPlan(TermPlan.Create(termPlanner, 1, new List<Subject> { TermPlannerHelpers.CreateSubject("English", "ENG005") }));

        // Assert
        termPlanner.TermPlans.Should().HaveCount(1);
        termPlanner.TermPlans[0].Should().Be(termPlan);
        act.Should().Throw<DuplicateTermNumberException>();
    }

    [Fact]
    public void AddSubjects_OnAddingSubject_ShouldAddSubject()
    {
        // Arrange
        var termPlanner = TermPlanner.Create(2021, YearLevelValue.Year1, null);
        var subject = Subject.Create("English", new List<YearLevel>());

        // Act
        termPlanner.AddSubject(subject);

        // Assert
        termPlanner.Subjects.Should().HaveCount(1);
        termPlanner.Subjects[0].Should().Be(subject);
    }
}