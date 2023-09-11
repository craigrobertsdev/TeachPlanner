using TeachPlanner.Domain.Subjects;
using TeachPlanner.Domain.WeekPlanners;

namespace TeachPlanner.Application.WeekPlanners.Queries.GetPlannerData;

public record GetPlannerDataResult(
  WeekPlanner WeekPlanner,
  DayPlanPattern DayPlanPattern,
  List<Subject> Subjects
);