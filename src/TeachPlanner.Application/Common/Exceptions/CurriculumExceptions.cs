using TeachPlanner.Domain.Common.Exceptions;

namespace TeachPlanner.Application.Common.Exceptions;
public class NoSubjectsFoundException : BaseException
{
    public NoSubjectsFoundException() : base("No subjects were found in the database", 404)
    {
    }
}
