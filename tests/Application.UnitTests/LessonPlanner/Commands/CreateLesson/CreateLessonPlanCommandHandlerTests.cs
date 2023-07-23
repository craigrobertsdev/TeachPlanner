using Application.Common.Interfaces.Persistence;
using Application.LessonPlan.CreateLessonPlan.Commands;
using Application.UnitTests.LessonPlanner.Commands.TestUtils;
using FluentAssertions;
using Moq;

namespace Application.UnitTests.LessonPlanner.Commands.CreateLesson;
public class CreateLessonPlanCommandHandlerTests
{
    private readonly CreateLessonPlanCommandHandler _handler;
    private readonly Mock<ILessonRepository> _mockLessonRepository;

    public CreateLessonPlanCommandHandlerTests()
    {
        _mockLessonRepository = new Mock<ILessonRepository>();
        _handler = new CreateLessonPlanCommandHandler(_mockLessonRepository.Object);
    }

    [Theory]
    [MemberData(nameof(ValidCreateLessonPlanCommands))]
    public async Task HandleCreateLessonPlanCommand_WhenLessonIsValid_ShouldCreateAndReturnLesson(CreateLessonPlanCommand command)
    {
        // Act
        ErrorOr.ErrorOr<Domain.LessonPlanAggregate.LessonPlan> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        // 1. Validate that the correct lessonplan was created
        result.IsError.Should().BeFalse();
        //result.Value.ValidateCreatedFrom
        _mockLessonRepository.Verify(l => l.Create(result.Value), Times.Once());
    }

    public static IEnumerable<object[]> ValidCreateLessonPlanCommands()
    {
        yield return new[] { CreateLessonPlanCommandUtils.CreateCommand() };

        yield return new[]
        {
            CreateLessonPlanCommandUtils.CreateCommand(resourceIds: CreateLessonPlanCommandUtils.CreateResourceIdList(3), assessmentIds: CreateLessonPlanCommandUtils.CreateAssessmentIdList(3))
        };

        yield return new[]
        {
            CreateLessonPlanCommandUtils.CreateCommand(assessmentIds: CreateLessonPlanCommandUtils.CreateAssessmentIdList(3))
        };

        yield return new[]
        {
            CreateLessonPlanCommandUtils.CreateCommand(resourceIds: CreateLessonPlanCommandUtils.CreateResourceIdList(3))
        };
    }
}
