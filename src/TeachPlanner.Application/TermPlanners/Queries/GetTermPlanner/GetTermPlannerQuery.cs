using MediatR;

namespace TeachPlanner.Application.TermPlanners.Queries.GetTermPlanner;
public record GetTermPlannerQuery(Guid TeacherId, int CalendarYear) : IRequest<GetTermPlannerResult>;
