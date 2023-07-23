using Application.Common.Interfaces.Persistence;
using Domain.Common.Assessment.ValueObjects;
using Domain.Common.Curriculum.ValueObjects;
using Domain.LessonPlanAggregate;
using Domain.Resource.ValueObjects;
using Domain.TeacherAggregate.ValueObjects;
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
            new TeacherId(Guid.Parse(command.TeacherId)),
            new SubjectId(Guid.Parse(command.SubjectId)),
            command.PlanningNotes,
            command.StartTime,
            command.EndTime,
            command.ResourceIds?.Select(resourceId => new ResourceId(Guid.Parse(resourceId))).ToList(),
            command.AssessmentIds?.Select(assessmentId => new AssessmentId(Guid.Parse(assessmentId))).ToList());

        await _lessonRepository.Create(lesson);

        return lesson;
    }
}
