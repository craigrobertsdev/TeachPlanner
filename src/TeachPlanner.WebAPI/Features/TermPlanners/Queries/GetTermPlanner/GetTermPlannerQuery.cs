using MediatR;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Features.TermPlanners.Queries.GetTermPlanner;
public record GetTermPlannerQuery(TeacherId TeacherId, int CalendarYear) : IRequest<GetTermPlannerResult>;
