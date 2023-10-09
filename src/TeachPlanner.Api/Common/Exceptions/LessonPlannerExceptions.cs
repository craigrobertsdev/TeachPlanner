namespace TeachPlanner.Api.Common.Exceptions;
public class LessonPlansNotFoundException : BaseException
{
    public LessonPlansNotFoundException() : base("No lesson plans were found", 404, "LessonPlanner.NotFound") { }
}

public class ConflictingLessonPlansException : BaseException
{
    public ConflictingLessonPlansException(int startPeriod)
        : base($"An existing LessonPlan already covers period {startPeriod}", 400, "LessonPlanner.Conflict") { }
}
