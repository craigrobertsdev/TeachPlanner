using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.Teachers.GetTeacherSettings;
using TeachPlanner.Api.Domain.Students;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.YearDataRecords;
using TeachPlanner.Api.Features.Teachers;
using TeachPlanner.Api.UnitTests.Helpers;

namespace TeachPlanner.Api.UnitTests.Features.Teachers;
public class GetTeacherSettingsTests {
    private readonly ITeacherRepository _teacherRepository;
    private readonly IYearDataRepository _yearDataRepository;
    private readonly ITermPlannerRepository _termPlannerRepository;

    public GetTeacherSettingsTests() {
        _teacherRepository = A.Fake<ITeacherRepository>();
        _yearDataRepository = A.Fake<IYearDataRepository>();
        _termPlannerRepository = A.Fake<ITermPlannerRepository>();
    }

    [Fact]
    public async void Handle_WhenPassedValidData_ReturnsGetTeacherSettingsResponse() {
        // Arrange
        var curriculumSubjects = SubjectHelpers.CreateCurriculumSubjects();
        var teacher = TeacherHelpers.CreateTeacher();
        var yearData = YearData.Create(teacher.Id, 2023);
        yearData.AddStudent(Student.Create(teacher.Id, "Fred", "Smith"));
        var yearDataEntry = YearDataEntry.Create(2023, yearData.Id);
        teacher.AddYearData(yearDataEntry);
        var calendarYear = 2023;
        var query = new GetTeacherSettings.Query(teacher.Id, calendarYear);
        var handler = new GetTeacherSettings.Handler(_teacherRepository, _yearDataRepository, _termPlannerRepository);

        A.CallTo(() => _teacherRepository.GetById(teacher.Id, new CancellationToken())).Returns(teacher);
        A.CallTo(() => _yearDataRepository.GetByTeacherIdAndYear(teacher.Id, query.CalendarYear, default)).Returns(yearData);

        // Act
        var result = await handler.Handle(query, new CancellationToken());

        // Assert
        result.Should().BeOfType<GetTeacherSettingsResponse>();
        result.Students.Should().HaveCount(1);
        result.YearDataId.Should().Be(teacher.GetYearData(calendarYear)!.Value);
    }
}
