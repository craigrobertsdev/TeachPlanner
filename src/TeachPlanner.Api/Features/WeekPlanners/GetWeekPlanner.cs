using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.WeekPlanners;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.WeekPlanners;

namespace TeachPlanner.Api.Features.WeekPlanners;

public static class GetWeekPlanner
{
    public record Query(TeacherId TeacherId, int WeekNumber, int TermNumber, int Year) : IRequest<WeekPlannerResponse>;

    public sealed class Handler : IRequestHandler<Query, WeekPlannerResponse>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IWeekPlannerRepository _weekPlannerRepository;
        private readonly IYearDataRepository _yearDataRepository;

        public Handler(ITeacherRepository teacherRepository, IWeekPlannerRepository weekPlannerRepository, IYearDataRepository yearDataRepository)
        {
            _teacherRepository = teacherRepository;
            _weekPlannerRepository = weekPlannerRepository;
            _yearDataRepository = yearDataRepository;
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

            // creating a term dates service to get the week start date
            // also need to update the dayplantemplate to include number of periods and breaks, and update the accountsetup request to include this
            var yearData = await _yearDataRepository.GetByTeacherIdAndYear(request.TeacherId, request.Year, cancellationToken);
            //var template = DayPlanTemplate.Create()

            //var weekPlanner = WeekPlanner.Create(yearData.Id, 1, request.WeekNumber, request.Year, dayPlanTemplate, termDatesService.GetWeekStart(request.WeekNumber));

            return new WeekPlannerResponse(
                new List<DayPlan>(),
                WeekPlannerTemplateDto.Create(DayPlanTemplate.Create(new List<TemplatePeriod>())),
                DateTime.Now,
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