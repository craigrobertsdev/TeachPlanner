using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Application.Teachers.Queries.GetTeacherSettings;
using TeachPlanner.Domain.Students;
using Xunit;

namespace TeachPlanner.Application.Tests.Teachers;
public class GetTeacherSettingsQueryHandlerTests
{

    private readonly ITeacherRepository _teacherRepository;
    private readonly ICurriculumRepository _curriculumRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetTeacherSettingsQueryHandlerTests()
    {
        _teacherRepository = A.Fake<ITeacherRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _curriculumRepository = A.Fake<ICurriculumRepository>();
    }

    [Fact]
    public async void Handle_WhenPassedValidData_ReturnsGetTeacherSettingsResult()
    {
        // Arrange
        var curriculumSubjects = Helpers.CreateCurriculumSubjects();
        var teacher = Helpers.CreateTeacher();
        teacher.AddYearData(2023, new List<Student>
        {
            Student.Create(teacher.Id, "Fred", "Smith")
        });
        var calendarYear = 2023;
        var query = new GetTeacherSettingsQuery(teacher.Id, calendarYear);
        var handler = new Application.Teachers.Queries.GetTeacherSettings.GetTeacherSettingsQueryHandler(_teacherRepository, _curriculumRepository, _unitOfWork);

        A.CallTo(() => _curriculumRepository.GetCurriculumSubjectNamesAndIds(new CancellationToken())).Returns(curriculumSubjects);
        A.CallTo(() => _teacherRepository.GetById(teacher.Id, new CancellationToken())).Returns(teacher);

        // Act
        var result = await handler.Handle(query, new CancellationToken());

        // Assert
        result.Should().BeOfType<GetTeacherSettingsResult>();
        result.YearData.Students.Should().HaveCount(1);
        result.YearData.Should().BeEquivalentTo(teacher.GetYearData(calendarYear));
    }
}
