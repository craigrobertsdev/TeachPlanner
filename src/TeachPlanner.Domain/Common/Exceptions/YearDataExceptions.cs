namespace TeachPlanner.Domain.Common.Exceptions;
public class TermPlannerAlreadyExistsException : BaseException
{
    public TermPlannerAlreadyExistsException()
        : base("Term planner already exists for this year.",
            400, "YearData.TermPlannerAlreadyExists")
    { }
}
