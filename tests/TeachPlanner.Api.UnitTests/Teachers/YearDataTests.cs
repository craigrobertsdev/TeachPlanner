using FluentAssertions;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;
using TeachPlanner.Api.UnitTests.Helpers;

namespace TeachPlanner.Api.UnitTests.Teachers;
public class YearDataTests
{
    [Fact]
    public void Create_OnValidInput_ShouldReturnYearData()
    {
        // Arrange

        // Act
        var yearData = YearData.Create(new TeacherId(Guid.NewGuid()), 2023);

        // Assert
        yearData.Should().BeOfType<YearData>();
        yearData.CalendarYear.Should().Be(2023);
    }

    [Fact]
    public void AddSubjects_WhenNoSubjectsAlreadyAdded_ShouldAddSubjects()
    {
        // Arrange
        var yearData = YearData.Create(new TeacherId(Guid.NewGuid()), 2023);
        var curriculumSubjects = SubjectHelpers.CreateCurriculumSubjects();
        var subjects = SubjectHelpers.CreateSubjects();

        // Act
        yearData.AddSubjects(curriculumSubjects);

        // Assert
        yearData.Subjects.Count.Should().Be(2);
        yearData.Subjects.Should().BeEquivalentTo(subjects);
    }
}
