using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.TermPlanners;

namespace TeachPlanner.Api.Features.TermPlanners.Queries.GetTermPlanner;
public record GetTermPlannerResult(TermPlanner TermPlanner, List<Subject> Subjects);

