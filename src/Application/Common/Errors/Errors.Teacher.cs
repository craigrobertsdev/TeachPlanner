using ErrorOr;

namespace Application.Common.Errors;

public static partial class Errors
{
    public static class Teacher
    {
        public static Error DuplicateUserId => Error.Conflict(code: "Teacher.DuplicateUser", description: "UserId is already associated with a teacher.");
    }
}
