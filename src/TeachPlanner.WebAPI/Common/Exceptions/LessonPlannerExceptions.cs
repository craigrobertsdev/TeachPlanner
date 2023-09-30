namespace TeachPlanner.Api.Common.Exceptions;
public class LessonPlansNotFoundException : BaseException
{
    public LessonPlansNotFoundException() : base("No lesson plans were found", 404, "LessonPlanner.NotFound")
    {
    }
}
