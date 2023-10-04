using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Database;
using TeachPlanner.Api.Database.QueryExtensions;
using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Features.Subjects;
using TeachPlanner.WebApi.Tests.Helpers;

namespace TeachPlanner.WebApi.Tests.Features.Subjects;
public class GetSubjectsTests
{
    private readonly ApplicationDbContext _context;

    public GetSubjectsTests()
    {
        _context = A.Fake<ApplicationDbContext>();
    }

    [Fact]
    public async void Handler_WhenNoElaborationsRequested_ShouldReturnCurruculumSubjectsWithoutElaborationsAsync()
    {
        // Arrange
        var subjects = SubjectHelpers.CreateCurriculumSubjects();
        var query = new GetCurriculumSubjects.Query(false);
        var handler = new GetCurriculumSubjects.Handler(_context);

        A.CallTo(() => _context.GetCurriculumSubjects(true, A<CancellationToken>._)).Returns(subjects);

        // Act
        var result = await handler.Handle(query, new CancellationToken());

        // Assert
        result.Should().BeEquivalentTo(subjects);
    }
}
