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

        public Handler(ITeacherRepository teacherRepository, IWeekPlannerRepository weekPlannerRepository)
        {
            _teacherRepository = teacherRepository;
            _weekPlannerRepository = weekPlannerRepository;
        }

        public async Task<WeekPlannerResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetById(request.TeacherId, cancellationToken);

            if (teacher is null) throw new TeacherNotFoundException();

            var weekPlanner = await _weekPlannerRepository.GetWeekPlanner(request.TeacherId, request.WeekNumber,
                request.TermNumber, request.Year, cancellationToken);

            if (weekPlanner is null)
            {
                throw new WeekPlannerNotFoundException();
            }

            return new WeekPlannerResponse(
                weekPlanner.DayPlans.ToList(),
                weekPlanner.WeekPlanPattern,
                weekPlanner.WeekStart,
                weekPlanner.WeekNumber
            );
        }
    }

    public static async Task<IResult> Delegate(int weekNumber, int termNumber, int year, ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new Query(new TeacherId(Guid.Empty), weekNumber, termNumber, year);

        var result = await sender.Send(query, cancellationToken);

        return Results.Ok(result);
    }
}