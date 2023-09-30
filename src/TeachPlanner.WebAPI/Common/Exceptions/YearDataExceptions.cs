namespace TeachPlanner.Api.Common.Exceptions;
public class TermPlannerAlreadyExistsException : BaseException
{
    public TermPlannerAlreadyExistsException()
        : base("Term planner already exists for this year.",
            400, "YearData.TermPlannerAlreadyExists")
    { }
}

public class YearDataNotFoundException : BaseException
{
    public YearDataNotFoundException()
        : base("No YearData found", 404, "YearData.NotFound")
    {
    }
}
