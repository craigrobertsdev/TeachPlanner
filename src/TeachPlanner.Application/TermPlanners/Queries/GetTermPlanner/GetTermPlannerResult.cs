using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.TermPlanners;

namespace TeachPlanner.Application.TermPlanners.Queries.GetTermPlanner;
public record GetTermPlannerResult(TermPlanner TermPlanner, List<Subject> Subjects);

