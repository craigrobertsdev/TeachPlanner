using Application.Common.Interfaces.Persistence;
using Domain.Assessments;
using Domain.ResourceAggregate;
using Domain.SubjectAggregates;
using Domain.TeacherAggregate;
using ErrorOr;
using MediatR;

namespace Application.LessonPlan.CreateLessonPlan.Commands;

public class CreateLessonPlanCommandHandler : IRequestHandler<CreateLessonPlanCommand, ErrorOr<Domain.LessonPlanAggregate.LessonPlan>>
{
    private readonly ILessonRepository _lessonRepository;

    public CreateLessonPlanCommandHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<ErrorOr<Domain.LessonPlanAggregate.LessonPlan>> Handle(CreateLessonPlanCommand command, CancellationToken cancellationToken)
    {
        var lesson = Domain.LessonPlanAggregate.LessonPlan.Create(
            Guid.NewGuid(),
            Guid.NewGuid(),
            command.PlanningNotes,
            command.StartTime,
            command.EndTime,
            command.ResourceIds?.Select(resourceId => Guid.Parse(resourceId)).ToList(),
            command.SummativeAssessmentIds?.Select(assessmentId => Guid.Parse(assessmentId)).ToList(),
            command.FormativeAssessmentIds?.Select(assessmentId => Guid.Parse(assessmentId)).ToList());

        await _lessonRepository.Create(lesson);

        return lesson;
    }
}
