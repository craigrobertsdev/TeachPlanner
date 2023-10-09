using FakeItEasy;
using FluentAssertions;
using Humanizer;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.LessonPlans.CreateLessonPlan;
using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Features.LessonPlans;

namespace TeachPlanner.Api.UnitTests.Features.LessonPlans;
public class CreateLessonPlanTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILessonPlanRepository _lessonPlanRepository;
    private readonly IAssessmentRepository _assessmentRepository;
    private readonly IResourceRepository _resourceRepository;

    public CreateLessonPlanTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _lessonPlanRepository = A.Fake<ILessonPlanRepository>();
        _assessmentRepository = A.Fake<IAssessmentRepository>();
        _resourceRepository = A.Fake<IResourceRepository>();
    }

    [Fact]
    public async void Handle_WhenCalledWithValidData_ShouldReturnCreateLessonPlanResponse()
    {
        // Arrange
        var command = new CreateLessonPlan.Command(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Planning Notes",
            new List<LessonPlanResource>(),
            new List<AssessmentId>(),
            DateTime.Now,
            DateTime.Now.AddHours(1),
            1);


        var handler = new CreateLessonPlan.Handler(_lessonPlanRepository, _assessmentRepository, _resourceRepository, _unitOfWork);
        var cancellationToken = CancellationToken.None;
        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        result.Should().BeOfType<CreateLessonPlanResponse>();
        A.CallTo(() => _unitOfWork.SaveChangesAsync(cancellationToken)).MustHaveHappenedOnceExactly();
    }
}
