using MediatR;

namespace TeachPlanner.Api.Features.WeekPlanners.Queries.GetWeekPlannerData
{
    public record GetPlannerDataQuery(string TeacherId) : IRequest<GetPlannerDataResult>;
}