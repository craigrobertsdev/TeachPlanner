using TeachPlanner.Api.Domain.CurriculumSubjects;
using TeachPlanner.Api.Domain.WeekPlanners;

namespace TeachPlanner.Api.Features.WeekPlanners.Queries.GetWeekPlannerData;

public record GetPlannerDataResult(
  WeekPlanner WeekPlanner,
  DayPlanPattern DayPlanPattern,
  List<CurriculumSubject> Subjects
);