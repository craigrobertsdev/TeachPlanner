using FluentAssertions;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.TermPlanners;
using TeachPlanner.Domain.Tests.Helpers;

namespace TeachPlanner.Domain.Tests.TermPlanners;
public class TermPlanTests
{
    [Fact]
    public void Create_OnValidInput_ShouldReturnTermPlan()
    {
        // Arrange
        var termPlanner = TermPlannerHelpers.CreateTermPlanner();
        var subject = TermPlannerHelpers.CreateSubject("English", "ENG001");

        // Act
        var termPlan = TermPlan.Create(termPlanner, 1, new List<Subject>() { subject });

        // Assert
        termPlan.Should().BeOfType<TermPlan>();
        termPlan.Subjects.Should().HaveCount(1);
        termPlan.Subjects[0].Name.Should().Be("English");
        termPlan.TermNumber.Should().Be(1);
    }

    [Fact]
    public void AddContentDescription_OnAddingContentDescription_ShouldBeAdded()
    {
        // Arrange
        var termPlanner = TermPlannerHelpers.CreateTermPlanner();
        var termPlan = TermPlannerHelpers.CreateTermPlan(termPlanner, "ENG001");
        var contentDescription = TermPlannerHelpers.CreateSubject("English", "ENG002").YearLevels[0].Strands[0].Substrands![0].ContentDescriptions[0];

        // Act
        termPlan.AddContentDescription(contentDescription);

        // Assert
        var contentDescriptions = termPlan.Subjects[0].YearLevels[0].Strands[0].Substrands![0].ContentDescriptions;
        contentDescriptions.Should().HaveCount(2);
        contentDescriptions[1].Should().BeEquivalentTo(contentDescription);
    }

    [Fact]
    public void AddCurriculumCode_OnAddingSubjectWithoutSubstrands_ShouldBeAdded()
    {
        // Arrange
        var termPlanner = TermPlannerHelpers.CreateTermPlanner();
        var termPlan = TermPlannerHelpers.CreateTermPlan(termPlanner, "MAT001", false);
        var contentDescription = TermPlannerHelpers.CreateSubjectWithoutSubstrands("Maths", "MAT002").YearLevels[0].Strands[0].ContentDescriptions![0];

        // Act
        termPlan.AddContentDescription(contentDescription);

        // Assert
        var contentDescriptions = termPlan.Subjects[0].YearLevels[0].Strands[0].ContentDescriptions!;
        contentDescriptions.Should().HaveCount(2);
        contentDescriptions[1].Should().BeEquivalentTo(contentDescription);
    }

    [Fact]
    public void AddCurriculumCode_OnAddingDuplicateCode_ShouldNotBeAdded()
    {
        // Arrange
        var termPlanner = TermPlannerHelpers.CreateTermPlanner();
        var termPlan = TermPlannerHelpers.CreateTermPlan(termPlanner, "English");
        var contentDescription = TermPlannerHelpers.CreateSubject("English", "ENG001").YearLevels[0].Strands[0].Substrands![0].ContentDescriptions[0];

        // Act
        Action act = () => termPlan.AddContentDescription(contentDescription);

        // Assert
        var contentDescriptions = termPlan.Subjects[0].YearLevels[0].Strands[0].Substrands![0].ContentDescriptions;
        contentDescriptions.Should().HaveCount(1);
    }

    [Fact] 
    public void AddCurriculumCode_OnAddingNewSubject_ShouldBeAdded()
    {
        // Arrange
        var termPlanner = TermPlannerHelpers.CreateTermPlanner();
        var termPlan = TermPlannerHelpers.CreateTermPlan(termPlanner, "English");
        var contentDescription = TermPlannerHelpers.CreateSubject("Maths", "MAT001").YearLevels[0].Strands[0].Substrands![0].ContentDescriptions[0];

        // Act
        termPlan.AddContentDescription(contentDescription);

        // Assert
        var contentDescriptions = termPlan.Subjects[0].YearLevels[0].Strands[0].Substrands![0].ContentDescriptions;
        contentDescriptions.Should().HaveCount(1);
        termPlan.Subjects.Should().HaveCount(2);
    }

    [Fact]
    public void AddCurriculumCode_OnAddingNewYearLevel_ShouldBeAdded()
    {
        // Arrange
        var termPlanner = TermPlannerHelpers.CreateTermPlanner();
        var termPlan = TermPlannerHelpers.CreateTermPlan(termPlanner, "English");
        var contentDescription = TermPlannerHelpers.CreateSubject("English", "ENG001", YearLevelValue.Year5).YearLevels[0].Strands[0].Substrands![0].ContentDescriptions[0];

        // Act
        termPlan.AddContentDescription(contentDescription);

        // Assert
        termPlan.Subjects[0].YearLevels.Should().HaveCount(2);
        termPlan.Subjects[0].YearLevels[1].Should().BeEquivalentTo(contentDescription.Substrand!.Strand.YearLevel);
    }

    [Fact]
    public void AddCurriculumCode_OnAddingNewStrand_ShouldBeAdded()
    {
        // Arrange
        var termPlanner = TermPlannerHelpers.CreateTermPlanner();
        var termPlan = TermPlannerHelpers.CreateTermPlan(termPlanner, "English");
        var contentDescription = TermPlannerHelpers.CreateSubject("English", "ENG001", "Prose").YearLevels[0].Strands[0].Substrands![0].ContentDescriptions[0];

        // Act
        termPlan.AddContentDescription(contentDescription);

        // Assert
        termPlan.Subjects[0].YearLevels[0].Strands.Should().HaveCount(2);
        termPlan.Subjects[0].YearLevels[0].Strands[1].Should().BeEquivalentTo(contentDescription.Substrand!.Strand);
    }

    [Fact]
    public void AddCurriculumCode_OnAddingNewSubstrand_ShouldBeAdded()
    {
        // Arrange
        var termPlanner = TermPlannerHelpers.CreateTermPlanner();
        var termPlan = TermPlannerHelpers.CreateTermPlan(termPlanner, "English");
        var contentDescription = TermPlannerHelpers.CreateSubject("English", "ENG002", "Grammar", "Creative Writing").YearLevels[0].Strands[0].Substrands![0].ContentDescriptions[0];

        // Act
        termPlan.AddContentDescription(contentDescription);

        // Assert
        termPlan.Subjects[0].YearLevels[0].Strands[0].Substrands!.Should().HaveCount(2);
        termPlan.Subjects[0].YearLevels[0].Strands[0].Substrands![1].Should().BeEquivalentTo(contentDescription.Substrand!);
    }
}
