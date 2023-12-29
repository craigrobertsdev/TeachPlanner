using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Blazor.Common.Interfaces.Persistence;
using TeachPlanner.Shared.Domain.PlannerTemplates;
using TeachPlanner.Shared.Domain.Teachers;
using TeachPlanner.Shared.Domain.WeekPlanners;
using TeachPlanner.Shared.Contracts.WeekPlanners;

namespace TeachPlanner.Blazor.Features.WeekPlanners;

public static class CreateWeekPlanner {
    public record Command(
        TeacherId TeacherId,
        DayPlanTemplate DayPlanTemplate,
        DateOnly WeekStart,
        int WeekNumber,
        int TermNumber,
        int Year) : IRequest<WeekPlanner>;

    public sealed class Handler : IRequestHandler<Command, WeekPlanner> {
        private readonly IWeekPlannerRepository _weekPlannerRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IWeekPlannerRepository weekPlannerRepository, ITeacherRepository teacherRepository, IUnitOfWork unitOfWork) {
            _weekPlannerRepository = weekPlannerRepository;
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<WeekPlanner> Handle(Command request, CancellationToken cancellationToken) {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

            if (teacher is null) {
                throw new TeacherNotFoundException();
            }

            var yearDataId = teacher.GetYearData(request.Year);

            if (yearDataId is null) {
                throw new YearDataNotFoundException();
            }

            var weekPlanner = WeekPlanner.Create(
                yearDataId,
                request.WeekNumber,
                request.TermNumber,
                request.Year,
                request.DayPlanTemplate.Id,
                request.WeekStart);

            _weekPlannerRepository.Add(weekPlanner);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return weekPlanner;
        }
    }

    public static async Task<IResult> Delegate(Guid teacherId, CreateWeekPlannerRequest request, ISender sender,
        CancellationToken cancellationToken) {
        //var dayPlanTemplate = DayPlanTemplate.Create(
        //        //request.WeekPlannerTemplate.DayPlanTemplateId.Periods
        //        //    .Select(x => new TemplatePeriod(
        //        //        Enum.Parse<PeriodType>(x.PeriodType),
        //        //        x.Name,
        //        //        x.StartTime,
        //        //        x.EndTime))
        //        //    .ToList())); 
        //        new());

        //var command = new Command(
        //    new TeacherId(teacherId),
        //    dayPlanTemplate,
        //    request.WeekStart,
        //    request.WeekNumber,
        //    request.TermNumber,
        //    request.Year);

        //var result = await sender.Send(command, cancellationToken);

        return Results.Ok();
    }
}
