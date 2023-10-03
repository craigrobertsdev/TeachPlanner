using TeachPlanner.Api.Domain.Subjects;
using TeachPlanner.Api.Domain.TermPlanners;

namespace TeachPlanner.Api.Contracts.TermPlanners.GetTermPlanner;
public record GetTermPlannerResponse(TermPlanner TermPlanner, List<Subject> Subjects);

