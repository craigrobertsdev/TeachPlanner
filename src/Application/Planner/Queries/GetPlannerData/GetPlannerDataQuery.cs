using ErrorOr;
using MediatR;

namespace TeachPlanner.Application.Planner.Queries.GetPlannerData
{
    public record GetPlannerDataQuery(string TeacherId) : IRequest<ErrorOr<GetPlannerDataResult>>;
}