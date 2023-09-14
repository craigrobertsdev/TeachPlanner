namespace TeachPlanner.Domain.Common.Exceptions;
public class TermPlannerCreationException : BaseException
{
    public TermPlannerCreationException() : base("TermPlanners must have at least one year level or a list of year levels", 400)
    {
    }
}
