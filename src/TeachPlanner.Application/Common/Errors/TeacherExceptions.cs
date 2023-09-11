namespace TeachPlanner.Application.Common.Errors;
public class TeacherNotFoundException : BaseException
{
    public TeacherNotFoundException() : base("No teacher found with those details", 404)
    {

    }
}
public class TeacherHasNoSubjectsException : BaseException
{
    public TeacherHasNoSubjectsException() : base("Teacher has no subjects", 404)
    {

    }
}
