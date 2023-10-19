using MediatR;
using TeachPlanner.Api.Common.Exceptions;
using TeachPlanner.Api.Common.Interfaces.Persistence;
using TeachPlanner.Api.Contracts.WeekPlanners;
using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;
using TeachPlanner.Api.Domain.WeekPlanners;

namespace TeachPlanner.Api.Features.WeekPlanners;

public static class CreateWeekPlanner
{
    public record Command(
        TeacherId TeacherId,
        WeekPlannerTemplate WeekPlannerTemplate,
        DateOnly WeekStart,
        int WeekNumber,
        int TermNumber,
        int Year) : IRequest<WeekPlanner>;
    
    public sealed class Handler : IRequestHandler<Command, WeekPlanner>
    {
        private readonly IWeekPlannerRepository _weekPlannerRepository;
        private readonly IYearDataRepository _yearDataRepository;
        
        public Handler(IWeekPlannerRepository weekPlannerRepository, ITermPlannerRepository termPlannerRepository, IYearDataRepository yearDataRepository)
        {
            _weekPlannerRepository = weekPlannerRepository;
            _yearDataRepository = yearDataRepository;
        }

        public async Task<WeekPlanner> Handle(Command request, CancellationToken cancellationToken)
        {
            var yearData = await _yearDataRepository.GetByTeacherIdAndYear(request.TeacherId, request.Year, cancellationToken);

            if (yearData is null)
            {
                throw new TermPlannerNotFoundException();
            }

            var weekPlanner = WeekPlanner.Create(
                yearData.Id,
                request.WeekNumber,
                request.TermNumber,
                request.Year,
                request.WeekPlannerTemplate,
                request.WeekStart);

            return weekPlanner;
        }
    }

    public static async Task<IResult> Delegate(CreateWeekPlannerRequest request, ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateWeekPlanner.Command(
            request.TeacherId,
            request.WeekPlannerTemplate,
            request.WeekStart,
            request.WeekNumber,
            request.TermNumber,
            request.Year);

        var result = await sender.Send(command, cancellationToken);
        
        return Results.Ok(result);
    }
}
