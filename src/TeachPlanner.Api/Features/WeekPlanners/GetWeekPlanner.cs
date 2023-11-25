using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Common.Interfaces.Services;
using TeachPlanner.Api.Contracts.WeekPlanners;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.WeekPlanners;
using TeachPlanner.Api.Services;

namespace TeachPlanner.Api.Features.WeekPlanners;

public static class GetWeekPlanner
{
    public record Query(TeacherId TeacherId, int WeekNumber, int TermNumber, int Year) : IRequest<WeekPlannerResponse>;

    public sealed class Handler : IRequestHandler<Query, WeekPlannerResponse>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IWeekPlannerRepository _weekPlannerRepository;
        private readonly IYearDataRepository _yearDataRepository;
        private readonly ITermDatesService _termDatesService;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITeacherRepository teacherRepository, IWeekPlannerRepository weekPlannerRepository, IYearDataRepository yearDataRepository, ITermDatesService termDatesService, IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _weekPlannerRepository = weekPlannerRepository;
            _yearDataRepository = yearDataRepository;
            _termDatesService = termDatesService;
            _unitOfWork = unitOfWork;
        }

        public async Task<WeekPlannerResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

            if (teacher is null) throw new TeacherNotFoundException();

            var weekPlanner = await _weekPlannerRepository.GetWeekPlanner(request.TeacherId, request.WeekNumber,
                request.TermNumber, request.Year, cancellationToken);

            if (weekPlanner is not null)
            {
                return new WeekPlannerResponse(
                    weekPlanner.DayPlans.ToList(),
                    WeekPlannerTemplateDto.Create(weekPlanner.DayPlanTemplate),
                    weekPlanner.WeekStart,
                    weekPlanner.WeekNumber
                );
            }

            // also need to update the dayplantemplate to include number of periods and breaks, and update the accountsetup request to include this
            var yearData = await _yearDataRepository.GetByTeacherIdAndYear(request.TeacherId, request.Year, cancellationToken);

            weekPlanner = WeekPlanner.Create(
                yearData!.Id,
                1,
                request.WeekNumber,
                request.Year,
                yearData.DayPlanTemplate,
                _termDatesService.GetWeekStart(request.TermNumber, request.WeekNumber));

            _weekPlannerRepository.Add(weekPlanner);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new WeekPlannerResponse(
                new List<DayPlan>(),
                WeekPlannerTemplateDto.Create(weekPlanner.DayPlanTemplate),
                weekPlanner.WeekStart,
                request.WeekNumber);
        }
    }

    public static async Task<IResult> Delegate(Guid teacherId, int week, int term, int year, ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new Query(new TeacherId(teacherId), week, term, year);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result);
    }
}