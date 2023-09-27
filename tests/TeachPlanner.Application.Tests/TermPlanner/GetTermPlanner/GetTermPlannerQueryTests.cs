using FakeItEasy;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using FluentValidation.TestHelper;
using Xunit;
using TeachPlanner.Application.TermPlanners.Queries.GetTermPlanner;
using TeachPlanner.Domain.Teachers;
using FluentAssertions;
using TeachPlanner.Application.Common.Exceptions;
using TeachPlanner.Domain.Common.Enums;
using TeachPlanner.Domain.TermPlanners;
using TeachPlanner.Application.Tests.Teachers;

namespace TeachPlanner.Application.Tests.TermPlanners.GetTermPlanner;
public class GetTermPlannerQueryTests
{
    private readonly ITermPlannerRepository _termPlannerRepository;
    private readonly ITeacherRepository _teacherRepository;

    public GetTermPlannerQueryTests()
    {
        _termPlannerRepository = A.Fake<ITermPlannerRepository>();
        _teacherRepository = A.Fake<ITeacherRepository>();
    }

    [Fact]
    public async void Handle_WhenCalledWithValidData_GetShouldReturnTermPlannerResult()
    {
        // Arrange
        var query = new GetTermPlannerQuery(Guid.NewGuid(), 2023);
        var teacher = Teacher.Create(Guid.NewGuid(), "Test", "Teacher");
        var termPlanner = TermPlanner.Create(teacher.Id, 2023, new List<YearLevelValue>());
        var subjects = Helpers.CreateCurriculumSubjects();
        teacher.AddTermPlanner(termPlanner);

        var handler = new GetTermPlannerQueryHandler(_termPlannerRepository, _teacherRepository);

        A.CallTo(() => _teacherRepository.GetById(query.TeacherId, default)).Returns(teacher);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeOfType<GetTermPlannerResult>();
        result.TermPlanner.Should().Be(termPlanner);
        result.Subjects.Should().BeEquivalentTo(subjects);
    }

    [Fact]
    public void Handle_WhenTeacherNotFound_ShouldThrowException()
    {
        // Arrange
        var query = new GetTermPlannerQuery(Guid.NewGuid(), 2023);
        A.CallTo(() => _teacherRepository.GetById(query.TeacherId, default)).Returns((Teacher)null);
        var handler = new GetTermPlannerQueryHandler(_termPlannerRepository, _teacherRepository);

        // Act
        Func<Task> act = () => handler.Handle(query, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<TeacherNotFoundException>();
    }
}
