using TeachPlanner.Domain.Common.Exceptions;

namespace TeachPlanner.Application.Common.Exceptions;
public class LessonPlansNotFoundException : BaseException
{
    public LessonPlansNotFoundException() : base("No lesson plans were found", 404)
    {
    }
}
