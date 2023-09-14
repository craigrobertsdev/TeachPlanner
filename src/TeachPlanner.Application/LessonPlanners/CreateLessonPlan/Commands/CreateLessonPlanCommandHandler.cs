using MediatR;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Application.LessonPlanners.CreateLessonPlan.Commands;

public class CreateLessonPlanCommandHandler : IRequestHandler<CreateLessonPlanCommand, LessonPlan>
{
    private readonly ILessonRepository _lessonRepository;

    public CreateLessonPlanCommandHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<LessonPlan> Handle(CreateLessonPlanCommand command, CancellationToken cancellationToken)
    {
        var lesson = LessonPlan.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            command.PlanningNotes,
            command.StartTime,
            command.EndTime,
            command.NumberOfPeriods,
            command.Resources);

        await _lessonRepository.Create(lesson, cancellationToken);

        return lesson;
    }
}
