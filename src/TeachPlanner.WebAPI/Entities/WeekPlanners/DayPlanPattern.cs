using TeachPlanner.Api.Entities.Common.Primatives;
using TeachPlanner.Api.Entities.LessonPlans;

namespace TeachPlanner.Api.Entities.WeekPlanners;

public class DayPlanPattern : ValueObject
{
    public WeekPlannerId WeekPlannerId { get; private set; }
    public List<LessonPlan> LessonPlans { get; private set; }

    public DayPlanPattern(
        WeekPlannerId weekPlannerId,
        List<LessonPlan> lessonPlans)
    {
        WeekPlannerId = weekPlannerId;
        LessonPlans = lessonPlans;
    }

    public void UpdateLessonPlans(List<LessonPlan> lessonPlans)
    {
        LessonPlans = lessonPlans;
    }

    public static DayPlanPattern Create(
        WeekPlannerId weekPlannerId,
        List<LessonPlan> lessonPlans)
    {
        return new DayPlanPattern(
            weekPlannerId,
            lessonPlans);
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return WeekPlannerId;
        yield return LessonPlans;
    }
}