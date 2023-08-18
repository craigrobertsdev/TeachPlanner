using Domain.SubjectAggregates;
using Domain.WeekPlannerAggregate;

namespace Application.Planner.Queries.GetPlannerData;

public record GetPlannerDataResult(
  WeekPlanner WeekPlanner,
  DayPlanPattern DayPlanPattern,
  List<Subject> Subjects
);