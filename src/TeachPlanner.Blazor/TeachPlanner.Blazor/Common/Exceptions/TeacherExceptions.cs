namespace TeachPlanner.Blazor.Common.Exceptions;

public class TeacherNotFoundException : BaseException {
    public TeacherNotFoundException() : base("No teacher found with those details", 404, "Teachers.NotFound") {
    }
}

public class TeacherHasNoSubjectsException : BaseException {
    public TeacherHasNoSubjectsException()
        : base("Teachers has no subjects", 404, "Teachers.NoSubjects") {
    }
}

public class NoNewSubjectsTaughtException : BaseException {
    public NoNewSubjectsTaughtException()
        : base("No new subjects taught", 404, "Teachers.NoNewSubjectsTaught") {
    }
}

public class CreateTimeFromDtoException : BaseException {
    public CreateTimeFromDtoException(string message)
        : base(message, 400, "Teachers.CreateTimeFromDto") {
    }
}