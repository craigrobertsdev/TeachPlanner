using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.PlannerTemplates;

namespace TeachPlanner.Api.Features.WeekPlanners.Queries.GetWeekPlannerData;

public record GetPlannerDataResult(
    WeekPlanner WeekPlanner,
    DayPlanTemplate DayPlanPattern,
    List<CurriculumSubject> Subjects
);