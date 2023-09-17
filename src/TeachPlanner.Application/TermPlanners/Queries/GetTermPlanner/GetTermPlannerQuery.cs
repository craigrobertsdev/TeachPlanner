using MediatR;

namespace TeachPlanner.Application.TermPlanners.Queries.GetTermPlanner;
public record GetTermPlannerQuery(Guid TeacherId, Guid TermPlannerId) : IRequest<GetTermPlannerResult>;
