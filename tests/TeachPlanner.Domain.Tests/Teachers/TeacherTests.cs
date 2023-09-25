using FluentAssertions;
using TeachPlanner.Domain.Students;
using TeachPlanner.Domain.Teachers;
using TeachPlanner.Domain.Tests.Helpers;

namespace TeachPlanner.Domain.Tests.Teachers;
public class TeacherTests
{
    [Fact]
    public void AddYearData_WhenPassingYear_AddsYearData()
    {
        // Arrange
        var teacher = TeacherHelpers.CreateTeacher();
        var year = 2023;

        // Act
        teacher.AddYearData(year);

        // Assert
        teacher.YearDataHistory.Should().HaveCount(1);
        teacher.YearDataHistory[0].CalendarYear.Should().Be(year);
    }

    [Fact]
    public void AddYearData_WhenPassingYearAndStudents_AddsYearData()
    {
        // Arrange
        var teacher = TeacherHelpers.CreateTeacher();
        var year = 2023;
        var students = new List<Student>
        {
            Student.Create(teacher.Id, "Fred", "Smith")
        };

        // Act
        teacher.AddYearData(year, students);

        // Assert
        teacher.YearDataHistory.Should().HaveCount(1);
        teacher.YearDataHistory[0].CalendarYear.Should().Be(year);
        teacher.YearDataHistory[0].Students.Should().HaveCount(1);
    }

    [Fact]
    public void AddYearData_WhenPassedNewYearData_AddsThatYearData()
    {
        // Arrange
        var teacher = TeacherHelpers.CreateTeacher();
        var yearData = YearData.Create(2023);

        // Act
        teacher.AddYearData(yearData);

        // Assert
        teacher.YearDataHistory.Should().HaveCount(1);
        teacher.YearDataHistory[0].CalendarYear.Should().Be(yearData.CalendarYear);
    }

    [Fact]
    public void AddYearData_WhenPassingDuplicateYearData_ShouldNotCreate()
    {
        // Arrange
        var teacher = TeacherHelpers.CreateTeacher();
        var yearData = YearData.Create(2023);

        // Act
        teacher.AddYearData(yearData);
        teacher.AddYearData(yearData);

        // Assert
        teacher.YearDataHistory.Should().HaveCount(1);
        teacher.YearDataHistory[0].CalendarYear.Should().Be(yearData.CalendarYear);
    }

    [Fact]
    public void GetYearData_WhenDataExists_ReturnsYearData()
    {
        // Arrange
        var teacher = TeacherHelpers.CreateTeacher();
        var year = 2023;
        teacher.AddYearData(year);

        // Act
        var result = teacher.GetYearData(year);

        // Assert
        result.Should().NotBeNull();
        result?.CalendarYear.Should().Be(year);
    }

    [Fact]
    public void AddSubjectsTaught_WhenYearDataExistsForYear_UpdateThatYearData()
    {
        // Arrange
        var subjects = SubjectHelpers.CreateCurriculumSubjects();
        var teacher = TeacherHelpers.CreateTeacher();
        teacher.AddYearData(2023);
        teacher.GetYearData(2023)!.AddSubjects(subjects.Take(3).ToList());

        // Act
        teacher.AddSubjectsTaught(subjects.Skip(3).ToList(), 2023);

        // Assert
        teacher.YearDataHistory.Should().HaveCount(1);
    }
}
