using Application.LessonPlan.CreateLessonPlan.Common;
using ErrorOr;
using MediatR;

namespace Application.LessonPlan.CreateLessonPlan.Commands;

public class CreateLessonPlanCommandHandler : IRequestHandler<CreateLessonPlanCommand, ErrorOr<CreateLessonPlanResult>>
{
    private readonly ILessonRepository _lessonRepository;

    public CreateLessonPlanCommandHandler(ILessonRepository lessonRepository)
    {
        _lessonRepository = lessonRepository;
    }

    public async Task<ErrorOr<CreateLessonPlanResult>> Handle(CreateLessonPlanCommand command, CancellationToken cancellationToken)
    {
        var lesson =
    }
}
