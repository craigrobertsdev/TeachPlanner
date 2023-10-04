using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Teachers.GetTeacherSettings;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.QueryExtensions;
using TeachPlanner.Api.Domain.Students;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;
using TeachPlanner.Api.Features.Teachers;
using TeachPlanner.WebApi.Tests.Helpers;

namespace TeachPlanner.WebApi.Tests.Features.Teachers;
public class GetTeacherSettingsQueryHandlerTests
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public GetTeacherSettingsQueryHandlerTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _context = A.Fake<ApplicationDbContext>();
    }

    [Fact]
    public async void Handle_WhenPassedValidData_ReturnsGetTeacherSettingsResult()
    {
        // Arrange
        var curriculumSubjects = SubjectHelpers.CreateCurriculumSubjects();
        var teacher = TeacherHelpers.CreateTeacher();
        var yearData = YearData.Create(teacher.Id, 2023);
        yearData.AddStudent(Student.Create(teacher.Id, "Fred", "Smith"));
        var yearDataEntry = YearDataEntry.Create(2023, yearData.Id);
        teacher.AddYearData(yearDataEntry);
        var calendarYear = 2023;
        var query = new GetTeacherSettings.Query(teacher.Id, calendarYear);
        var handler = new GetTeacherSettings.Handler(_context, _unitOfWork);

        A.CallTo(() => _context.GetTeacherById(teacher.Id, new CancellationToken())).Returns(teacher);

        // Act
        var result = await handler.Handle(query, new CancellationToken());

        // Assert
        result.Should().BeOfType<GetTeacherSettingsResponse>();
        result.Students.Should().HaveCount(1);
        result.YearDataId.Should().Be(teacher.GetYearData(calendarYear)!.Value);
    }
}
