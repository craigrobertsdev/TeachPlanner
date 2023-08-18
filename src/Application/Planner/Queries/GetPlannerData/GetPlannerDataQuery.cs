using ErrorOr;
using MediatR;

namespace Application.Planner.Queries.GetPlannerData
{
    public record GetPlannerDataQuery(string TeacherId) : IRequest<ErrorOr<GetPlannerDataResult>>;
}