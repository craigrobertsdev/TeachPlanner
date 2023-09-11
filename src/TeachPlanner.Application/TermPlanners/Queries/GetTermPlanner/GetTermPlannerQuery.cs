using MediatR;

namespace TeachPlanner.Application.TermPlanners.Queries.GetTermPlanner;
public record GetTermPlannerQuery(Guid TeacherId, Guid TermId) : IRequest<GetTermPlannerResult>;
