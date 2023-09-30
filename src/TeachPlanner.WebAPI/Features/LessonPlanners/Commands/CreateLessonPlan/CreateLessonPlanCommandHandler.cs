using MediatR;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Entities.LessonPlans;
using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.Teachers;
using TeachPlanner.Api.Entities.YearDataRecords;

namespace TeachPlanner.Api.Features.LessonPlanners.Commands.CreateLessonPlan;

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
            new TeacherId(Guid.NewGuid()),
            new YearDataId(Guid.NewGuid()),
            new SubjectId(command.SubjectId),
            command.PlanningNotes,
            command.StartTime,
            command.EndTime,
            command.NumberOfPeriods,
            command.Resources);

        _lessonPlanRepository.Add(lesson);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
