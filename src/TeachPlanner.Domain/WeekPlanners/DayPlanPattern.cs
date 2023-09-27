using TeachPlanner.Domain.Common.Primatives;
using TeachPlanner.Domain.LessonPlans;

namespace TeachPlanner.Domain.WeekPlanners;

public class DayPlanPattern : Entity
{
    public Guid WeekPlannerId { get; private set; }
    public List<LessonPlan> LessonPlans { get; private set; }

    public DayPlanPattern(
        Guid id,
        Guid weekPlannerId,
        List<LessonPlan> lessonPlans) : base(id)
    {
        WeekPlannerId = weekPlannerId;
        LessonPlans = lessonPlans;
    }

    public void UpdateLessonPlans(List<LessonPlan> lessonPlans)
    {
        LessonPlans = lessonPlans;
    }

    public static DayPlanPattern Create(
        Guid weekPlannerId,
        List<LessonPlan> lessonPlans)
    {
        return new DayPlanPattern(
            Guid.NewGuid(),
            weekPlannerId,
            lessonPlans);
    }
}