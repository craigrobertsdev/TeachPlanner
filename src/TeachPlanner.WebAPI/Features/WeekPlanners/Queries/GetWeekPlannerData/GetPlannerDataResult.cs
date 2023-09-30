using TeachPlanner.Api.Entities.Subjects;
using TeachPlanner.Api.Entities.WeekPlanners;

namespace TeachPlanner.Api.Features.WeekPlanners.Queries.GetWeekPlannerData;

public record GetPlannerDataResult(
  WeekPlanner WeekPlanner,
  DayPlanPattern DayPlanPattern,
  List<Subject> Subjects
);