namespace TeachPlanner.Api.Common.Exceptions;

public class TooManyDayPlansInWeekPlannerException : BaseException
{
    public TooManyDayPlansInWeekPlannerException()
        : base("A week planner can only have 5 day plans", 400, "WeekPlanner.TooManyDayPlans")
    {
    }
}