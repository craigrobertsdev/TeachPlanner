using ErrorOr;
using MediatR;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Application.LessonPlans.CreateLessonPlan.Commands;

public class CreateLessonPlanCommandHandler : IRequestHandler<CreateLessonPlanCommand, ErrorOr<LessonPlan>>
{
    private readonly ILessonRepository _lessonRepository;

    public CreateLessonPlanCommandHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<ErrorOr<LessonPlan>> Handle(CreateLessonPlanCommand command, CancellationToken cancellationToken)
    {
        var lesson = LessonPlan.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            command.PlanningNotes,
            command.StartTime,
            command.EndTime,
            command.NumberOfPeriods,
            command.Resources,
            command.SummativeAssessmentIds?.Select(assessmentId => Guid.Parse(assessmentId)).ToList(),
            command.FormativeAssessmentIds?.Select(assessmentId => Guid.Parse(assessmentId)).ToList());

        await _lessonRepository.Create(lesson);

        return lesson;
    }
}
