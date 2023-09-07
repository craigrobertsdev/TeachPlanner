using ErrorOr;

namespace TeachPlanner.Application.Common.Errors;

public static partial class Errors
{
    public static class Teacher
    {
        public static Error TeacherNotFound => Error.NotFound("No teacher found with that ID");
        public static Error TeacherHasNoSubjects => Error.NotFound("Teacher has no subjects");
    }
}
