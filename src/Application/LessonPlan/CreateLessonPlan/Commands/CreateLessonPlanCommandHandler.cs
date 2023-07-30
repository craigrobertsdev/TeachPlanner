using Application.Common.Interfaces.Persistence;
using Domain.Assessments.ValueObjects;
using Domain.ResourceAggregate.ValueObjects;
using Domain.SubjectAggregates.ValueObjects;
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
            TeacherIdForReference.Create(Guid.Parse(command.TeacherId)),
            SubjectIdForReference.Create(Guid.Parse(command.SubjectId)),
            command.PlanningNotes,
            command.StartTime,
            command.EndTime,
            command.ResourceIds?.Select(resourceId => ResourceIdForReference.Create(Guid.Parse(resourceId))).ToList(),
            command.AssessmentIds?.Select(assessmentId => AssessmentIdForReference.Create(Guid.Parse(assessmentId))).ToList());

        await _lessonRepository.Create(lesson);

        return lesson;
    }
}
