using MediatR;
using TeachPlanner.Api.Entities.Teachers;

namespace TeachPlanner.Api.Features.TermPlanners.Queries.GetTermPlanner;
public record GetTermPlannerQuery(TeacherId TeacherId, int CalendarYear) : IRequest<GetTermPlannerResult>;
