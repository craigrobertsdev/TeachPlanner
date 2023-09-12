using TeachPlanner.Domain.Common.Exceptions;

namespace TeachPlanner.Application.Common.Exceptions;

public class InvalidCredentialsException : BaseException
{
    public InvalidCredentialsException() : base("Invalid credentials", 401)
    {

    }
}

public class DuplicateEmailException : BaseException
{
    public DuplicateEmailException() : base("Email already exists", 409)
    {

    }
}

public class DuplicateIdException : BaseException
{
    public DuplicateIdException() : base("A user with this id already exists", 409)
    {

    }
}
