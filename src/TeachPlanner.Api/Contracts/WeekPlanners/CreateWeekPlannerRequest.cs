using TeachPlanner.Api.Domain.PlannerTemplates;
using TeachPlanner.Api.Domain.Teachers;

namespace TeachPlanner.Api.Contracts.WeekPlanners;

public record CreateWeekPlannerRequest(
    int WeekNumber, 
    int TermNumber,
    int Year,
    WeekPlannerTemplateDto WeekPlannerTemplate,
    DateTime WeekStart);