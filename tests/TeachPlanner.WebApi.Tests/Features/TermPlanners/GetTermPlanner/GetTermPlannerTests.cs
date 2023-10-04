using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.Users;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Features.TermPlanners;
using TeachPlanner.Api.Domain.TermPlanners;
using TeachPlanner.Api.Domain.Common.Enums;
using TeachPlanner.Api.Contracts.TermPlanners.GetTermPlanner;
using TeachPlanner.Api.Domain.YearDataRecords;
using TeachPlanner.WebApi.Tests.Helpers;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Database.QueryExtensions;

namespace TeachPlanner.WebApi.Tests.Features.TermPlanners.GetTermPlannerTests;
public class GetTermPlannerTests
{
    private readonly ApplicationDbContext _context;

    public GetTermPlannerTests()
    {
        _context = A.Fake<ApplicationDbContext>();
    }

    [Fact]
    public async void Handle_WhenCalledWithValidData_GetShouldReturnTermPlannerResult()
    {
        // Arrange
        var query = new GetTermPlanner.Query(new TeacherId(Guid.NewGuid()), 2023);
        var teacher = Teacher.Create(new UserId(Guid.NewGuid()), "Test", "Teacher");
        var termPlanner = TermPlanner.Create(new YearDataId(Guid.NewGuid()), 2023, new List<YearLevelValue>());
        var subjects = SubjectHelpers.CreateCurriculumSubjects();

        var handler = new GetTermPlanner.Handler(_context);

        A.CallTo(() => _context.GetTeacherById(query.TeacherId, default)).Returns(teacher);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeOfType<GetTermPlannerResponse>();
        result.TermPlanner.Should().Be(termPlanner);
        result.Subjects.Should().BeEquivalentTo(subjects);
    }

    [Fact]
    public void Handle_WhenTeacherNotFound_ShouldThrowException()
    {
        // Arrange
        var query = new GetTermPlanner.Query(new TeacherId(Guid.NewGuid()), 2023);
        A.CallTo(() => _context.GetTeacherById(query.TeacherId, default)).Returns((Teacher)null!);
        var handler = new GetTermPlanner.Handler(_context);

        // Act
        Func<Task> act = () => handler.Handle(query, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<TeacherNotFoundException>();
    }
}
