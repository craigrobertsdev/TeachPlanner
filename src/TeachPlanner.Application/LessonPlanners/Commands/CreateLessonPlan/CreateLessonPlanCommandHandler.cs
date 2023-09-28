using MediatR;
using TeachPlanner.Application.Common.Interfaces.Persistence;
using TeachPlanner.Domain.LessonPlans;
using TeachPlanner.Domain.YearDataRecords;

namespace TeachPlanner.Application.LessonPlanners.Commands.CreateLessonPlan;

public class CreateLessonPlanCommandHandler : IRequestHandler<CreateLessonPlanCommand>
{
    private readonly ILessonPlannerRepository _lessonPlanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLessonPlanCommandHandler(ILessonPlannerRepository lessonPlanRepository, IUnitOfWork unitOfWork)
    {
        _lessonPlanRepository = lessonPlanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateLessonPlanCommand command, CancellationToken cancellationToken)
    {
        var lesson = LessonPlan.Create(
            Guid.NewGuid(),
            new YearDataId(Guid.NewGuid()),
            Guid.Parse(command.SubjectId),
            command.PlanningNotes,
            command.StartTime,
            command.EndTime,
            command.NumberOfPeriods,
            command.Resources);

        _lessonPlanRepository.Add(lesson);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
