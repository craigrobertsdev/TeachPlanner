using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.TermPlanners;

namespace TeachPlanner.Api.Features.TermPlanners.Queries.GetTermPlanner;
public record GetTermPlannerResult(TermPlanner TermPlanner, List<Subject> Subjects);

