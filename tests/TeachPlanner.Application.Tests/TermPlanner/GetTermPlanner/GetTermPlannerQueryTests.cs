using FakeItEasy;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using FluentValidation.TestHelper;
using Xunit;
using TeachPlanner.Application.TermPlanners.Queries.GetTermPlanner;

namespace TeachPlanner.Application.Tests.TermPlanner.GetTermPlanner;
public class GetTermPlannerQueryTests
{
    private readonly ITermPlannerRepository _termPlannerRepositoryFake;
    public GetTermPlannerQueryTests()
    {
        _termPlannerRepositoryFake = A.Fake<ITermPlannerRepository>();
    }
    [Fact]
    public void GetTermPlannerQuery_WhenNoIdProvided_ShouldReturnValidationProblem()
    {
        // Arrange
        var query = new GetTermPlannerQuery(Guid.NewGuid(), Guid.NewGuid());

        // Act

        // Assert
    }
}
