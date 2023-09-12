namespace TeachPlanner.Domain.Common.Exceptions;
public class TermPlannerCreationException : BaseException
{
    public TermPlannerCreationException() : base("TermPlanner must have at least one year level or a list of year levels", 400)
    {
    }
}
