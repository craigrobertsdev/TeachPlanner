namespace Application.Common.Exceptions.Authentication;

public class DuplicateUserException : Exception
{
    public new string Message { get; set; }
    public DuplicateUserException()
    {
        Message = "User already exists";
    }
}
