using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Contracts.WeekPlanners;

public record CreateWeekPlannerRequest(TeacherId TeacherId,
    int WeekNumber, 
    int TermNumber,
    int Year,
    WeekPlannerTemplate WeekPlannerTemplate,
    DateOnly WeekStart);