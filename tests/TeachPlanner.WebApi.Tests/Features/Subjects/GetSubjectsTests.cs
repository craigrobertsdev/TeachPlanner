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
    private readonly ISubjectRepository _subjectRepository;

    public GetSubjectsTests()
    {
        _subjectRepository = A.Fake<ISubjectRepository>();
    }

    [Fact]
    public async void Handler_WhenNoElaborationsRequested_ShouldReturnCurriculumSubjectsWithoutElaborations()
    {
        // Arrange
        var subjects = SubjectHelpers.CreateCurriculumSubjects();
        var query = new GetCurriculumSubjects.Query(false);
        var handler = new GetCurriculumSubjects.Handler(_subjectRepository);

        A.CallTo(() => _subjectRepository.GetCurriculumSubjects(query.IncludeElaborations, A<CancellationToken>._)).Returns(subjects);

        // Act
        var result = await handler.Handle(query, new CancellationToken());

        // Assert
        result.Should().BeEquivalentTo(subjects);
    }
}
