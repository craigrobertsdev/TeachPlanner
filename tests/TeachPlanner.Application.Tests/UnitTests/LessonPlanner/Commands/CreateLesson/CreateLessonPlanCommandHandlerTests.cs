﻿using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Application.LessonPlanners.CreateLessonPlan.Commands;
using FluentAssertions;

namespace TeachPlanner.Application.Tests.UnitTests.LessonPlanner.Commands.CreateLesson;
public class CreateLessonPlanCommandHandlerTests
{
    /*    private readonly CreateLessonPlanCommandHandler _handler;
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
    */
    /*    public static IEnumerable<object[]> ValidCreateLessonPlanCommands()
        {
            yield return new[] { CreateLessonPlanCommandUtils.CreateCommand() };

            yield return new[]
            {
                CreateLessonPlanCommandUtils.CreateCommand(resourceIds: CreateLessonPlanCommandUtils.CreateResourceIdList(3), summativeAssessmentIds: CreateLessonPlanCommandUtils.CreateAssessmentIdList(3))
            };

            yield return new[]
            {
                CreateLessonPlanCommandUtils.CreateCommand(summativeAssessmentIds: CreateLessonPlanCommandUtils.CreateAssessmentIdList(3))
            };

            yield return new[]
            {
                CreateLessonPlanCommandUtils.CreateCommand(resourceIds: CreateLessonPlanCommandUtils.CreateResourceIdList(3))
            };
        }
    */
}
