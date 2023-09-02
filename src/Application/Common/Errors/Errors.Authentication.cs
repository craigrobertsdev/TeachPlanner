using ErrorOr;

namespace TeachPlanner.Application.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCredentials",
            description: "Invalid Credentials");

        public static Error DuplicateEmail => Error.Conflict(code: "Auth.DuplicateEmail", description: "Email already exists");
        public static Error DuplicateId => Error.Conflict(code: "Auth.DuplicateId", description: "A user with this id already exists");
    }
}
