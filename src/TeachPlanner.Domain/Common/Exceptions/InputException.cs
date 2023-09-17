namespace TeachPlanner.Domain.Common.Exceptions;
public class InputException : BaseException
{
    public InputException(string message) : base(message, 400, "Input.Error")
    {
    }
}
