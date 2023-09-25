using FluentAssertions;
using TeachPlanner.Domain.Common.Exceptions;
using TeachPlanner.Domain.Teachers;
using TeachPlanner.Domain.Tests.Helpers;

namespace TeachPlanner.Domain.Tests.Teachers;
public class YearDataTests
{
    [Fact]
    public void Create_OnValidInput_ShouldReturnYearData()
    {
        // Arrange

        // Act
        var yearData = YearData.Create(2023);

        // Assert
        yearData.Should().BeOfType<YearData>();
        yearData.CalendarYear.Should().Be(2023);
    }

    [Fact]
    public void AddSubjects_WhenNoSubjectsAlreadyAdded_ShouldAddSubjects()
    {
        // Arrange
        var yearData = YearData.Create(2023);
        var subjects = SubjectHelpers.CreateCurriculumSubjects();

        // Act
        yearData.AddSubjects(subjects);

        // Assert
        yearData.Subjects.Count.Should().Be(10);
        yearData.Subjects.Should().BeEquivalentTo(subjects);
    }

    [Fact]
    public void AddSubjects_WhenPassingNonCurriculumSubject_ShouldThrowException()
    {
        // Arrange
        var subjects = SubjectHelpers.CreateSubjects();
        var yearData = YearData.Create(2023);

        // Act
        Action act = () => yearData.AddSubjects(subjects);

        // Assert
        act.Should().Throw<IsNonCurriculumSubjectException>();

    }
}
