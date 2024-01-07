namespace TeachPlanner.Shared.Common.Exceptions;

public class InvalidCredentialsException : BaseException {
    public InvalidCredentialsException() : base("Invalid credentials", 401, "Authentication.InvalidCredentials") {
    }
}

public class DuplicateEmailException : BaseException {
    public DuplicateEmailException() : base("That email is already in use", 409, "Authentication.DuplicateEmail") {
    }
}

public class DuplicateIdException : BaseException {
    public DuplicateIdException() : base("A user with this id already exists", 409, "Authentication.DuplicateId") {
    }
}

public class UserRegistrationFailedException : BaseException {
    public UserRegistrationFailedException(string? message = null) : base(
        message ?? "Users registration failed. Please check details and try again.", 500, "Authentication.UserRegistrationFailed") {
    }
}

public class PasswordsDoNotMatchException : BaseException {
    public PasswordsDoNotMatchException() : base("Passwords do not match", 400, "Authentication.PasswordsDoNotMatch") {
    }
}