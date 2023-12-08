using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;
using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Contracts.TermPlanners.GetTermPlanner;
using TeachPlanner.Api.Domain.YearDataRecords;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.UnitTests.Helpers;
using TeachPlanner.Api.Features.TermPlanners;

namespace TeachPlanner.Api.UnitTests.Features.TermPlanners.GetTermPlannerTests;
public class GetTermPlannerTests {
    private readonly ITermPlannerRepository _termPlannerRepository;
    private readonly ITeacherRepository _teacherRepository;

    public GetTermPlannerTests() {
        _termPlannerRepository = A.Fake<ITermPlannerRepository>();
        _teacherRepository = A.Fake<ITeacherRepository>();
    }

    [Fact]
    public async void Handle_WhenCalledWithValidData_GetShouldReturnTermPlannerResult() {
        // Arrange
        var query = new GetTermPlanner.Query(new TeacherId(Guid.NewGuid()), 2023);
        var teacher = Teacher.Create(new UserId(Guid.NewGuid()), "Test", "Teacher");
        var yearData = YearData.Create(teacher.Id, 2023, DayPlanTemplateHelpers.CreateDayPlanTemplate(teacher.Id)); 
        teacher.AddYearData(YearDataEntry.Create(2023, yearData.Id));
        var termPlanner = TermPlanner.Create(new YearDataId(Guid.NewGuid()), 2023, new List<YearLevelValue>());
        var subjects = SubjectHelpers.CreateCurriculumSubjects();

        var handler = new GetTermPlanner.Handler(_termPlannerRepository, _teacherRepository);

        A.CallTo(() => _teacherRepository.GetById(query.TeacherId, default)).Returns(teacher);
        A.CallTo(() => _termPlannerRepository.GetByYearDataIdAndYear(yearData.Id, 2023, A<CancellationToken>._)).Returns(termPlanner);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeOfType<GetTermPlannerResponse>();
        result.TermPlanner.Should().Be(termPlanner);
    }

    [Fact]
    public void Handle_WhenTeacherNotFound_ShouldThrowException() {
        // Arrange
        var query = new GetTermPlanner.Query(new TeacherId(Guid.NewGuid()), 2023);
        A.CallTo(() => _teacherRepository.GetById(query.TeacherId, default)).Returns((Teacher)null!);
        var handler = new GetTermPlanner.Handler(_termPlannerRepository, _teacherRepository);

        // Act
        Func<Task> act = () => handler.Handle(query, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<TeacherNotFoundException>();
    }
}
