using FakeItEasy;
using FluentAssertions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.LessonPlans.CreateLessonPlan;
using TeachPlanner.Api.Domain.Assessments;
using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.LessonPlans;
using TeachPlanner.Api.Domain.YearDataRecords;
using TeachPlanner.Api.Features.LessonPlans;

namespace TeachPlanner.Api.UnitTests.Features.LessonPlans;
public class CreateLessonPlanTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILessonPlanRepository _lessonPlanRepository;
    private readonly IAssessmentRepository _assessmentRepository;

    public CreateLessonPlanTests()
    {
        _unitOfWork = A.Fake<IUnitOfWork>();
        _lessonPlanRepository = A.Fake<ILessonPlanRepository>();
        _assessmentRepository = A.Fake<IAssessmentRepository>();
    }

    [Fact]
    public async void Handle_WhenCalledWithValidData_ShouldReturnCreateLessonPlanResponse()
    {
        // Arrange
        var command = new CreateLessonPlan.Command(
            new YearDataId(Guid.NewGuid()),
            new SubjectId(Guid.NewGuid()),
            new List<string>(),
            "Planning Notes",
            new DateOnly(2023, 10, 10),
            1,
            1,
            new List<LessonPlanResource>(),
            new List<AssessmentId>());


        var handler = new CreateLessonPlan.Handler(_lessonPlanRepository, _assessmentRepository, _unitOfWork);
        var cancellationToken = CancellationToken.None;
        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        result.Should().BeOfType<CreateLessonPlanResponse>();
        A.CallTo(() => _unitOfWork.SaveChangesAsync(cancellationToken)).MustHaveHappenedOnceExactly();
    }
}
