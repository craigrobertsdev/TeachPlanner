using TeachPlanner.Api.Domain.LessonPlans;

namespace TeachPlanner.Api.Domain.WeekPlanners;

public record DayPlanPattern
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
}