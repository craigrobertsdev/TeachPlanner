using TeachPlanner.Domain.Common.Exceptions;

namespace TeachPlanner.Application.Common.Exceptions;
public class TeacherNotFoundException : BaseException
{
    public TeacherNotFoundException() : base("No teacher found with those details", 404, "Teacher.NotFound")
    {

    }
}
public class TeacherHasNoSubjectsException : BaseException
{
    public TeacherHasNoSubjectsException()
        : base("Teacher has no subjects", 404, "Teacher.NoSubjects")
    { }
}

public class NoNewSubjectsTaughtException : BaseException
{
    public NoNewSubjectsTaughtException()
        : base("No new subjects taught", 404, "Teacher.NoNewSubjectsTaught")
    { }
}

