using MediatR;

namespace TeachPlanner.Application.WeekPlanners.Queries.GetPlannerData
{
    public record GetPlannerDataQuery(string TeacherId) : IRequest<GetPlannerDataResult>;
}