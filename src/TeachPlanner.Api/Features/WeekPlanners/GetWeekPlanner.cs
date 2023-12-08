using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Common.Interfaces.Services;
using TeachPlanner.Api.Contracts.WeekPlanners;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.WeekPlanners;

namespace TeachPlanner.Api.Features.WeekPlanners;

public static class GetWeekPlanner {
    public record Query(TeacherId TeacherId, int WeekNumber, int TermNumber, int Year) : IRequest<WeekPlannerResponse>;

    public sealed class Handler : IRequestHandler<Query, WeekPlannerResponse> {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IWeekPlannerRepository _weekPlannerRepository;
        private readonly IYearDataRepository _yearDataRepository;
        private readonly ITermDatesService _termDatesService;
        private readonly IPlannerTemplateRepository _plannerTemplateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITeacherRepository teacherRepository, IWeekPlannerRepository weekPlannerRepository, IYearDataRepository yearDataRepository, ITermDatesService termDatesService, IUnitOfWork unitOfWork, IPlannerTemplateRepository plannerTemplateRepository) {
            _teacherRepository = teacherRepository;
            _weekPlannerRepository = weekPlannerRepository;
            _yearDataRepository = yearDataRepository;
            _termDatesService = termDatesService;
            _plannerTemplateRepository = plannerTemplateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<WeekPlannerResponse> Handle(Query request, CancellationToken cancellationToken) {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);
            if (teacher is null) {
                throw new TeacherNotFoundException();
            }

            var weekPlanner = await _weekPlannerRepository.GetWeekPlanner(request.WeekNumber, request.TermNumber, request.Year, cancellationToken);
            var dayPlanTemplate = await _plannerTemplateRepository.GetByTeacherId(teacher.Id, cancellationToken);

            if (weekPlanner is not null) {
                return new WeekPlannerResponse(
                    weekPlanner.DayPlans.ToList(),
                    DayPlanTemplateDto.Create(dayPlanTemplate!),
                    weekPlanner.WeekStart,
                    weekPlanner.WeekNumber
                );
            }

            var yearData = await _yearDataRepository.GetByTeacherIdAndYear(request.TeacherId, request.Year, cancellationToken);

            weekPlanner = WeekPlanner.Create(
                yearData!.Id,
                1,
                request.WeekNumber,
                request.Year,
                dayPlanTemplate!.Id,
                _termDatesService.GetWeekStart(request.TermNumber, request.WeekNumber));

            _weekPlannerRepository.Add(weekPlanner);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new WeekPlannerResponse(
                new List<DayPlan>(),
                DayPlanTemplateDto.Create(dayPlanTemplate),
                weekPlanner.WeekStart,
                request.WeekNumber);
        }
    }

    public static async Task<IResult> Delegate(Guid teacherId, int week, int term, int year, ISender sender,
        CancellationToken cancellationToken) {
        var query = new Query(new TeacherId(teacherId), week, term, year);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result);
    }
}