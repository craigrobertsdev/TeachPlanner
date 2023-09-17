using TeachPlanner.Domain.Common.Exceptions;

namespace TeachPlanner.Application.Common.Exceptions;

public class InvalidCredentialsException : BaseException
{
    public InvalidCredentialsException() : base("Invalid credentials", 401, "Authentication.InvalidCredentials")
    {

    }
}

public class DuplicateEmailException : BaseException
{
    public DuplicateEmailException() : base("That email is already in use", 409, "Authentication.DuplicateEmail")
    {

    }
}

public class DuplicateIdException : BaseException
{
    public DuplicateIdException() : base("A user with this id already exists", 409, "Authentication.DuplicateId")
    {

    }
}
