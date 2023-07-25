using Domain.Common.Primatives;
using Domain.LessonPlanAggregate.ValueObjects;

namespace Domain.LessonPlanAggregate.Entities;

public sealed class LessonComment : Entity<CommentId>
{
    public string Content { get; private set; }
    public bool Completed { get; private set; }
    public bool StruckThrough { get; private set; }
    public DateTime? CompletedDateTime { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private LessonComment(
        CommentId id, string content, bool completed, bool struckThrough, DateTime createdDateTime, DateTime updatedDateTime, DateTime? completedDateTime)
        : base(id)
    {
        Content = content;
        Completed = completed;
        StruckThrough = struckThrough;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        CompletedDateTime = completedDateTime;
    }

    public static LessonComment Create(string content)
    {
        return new LessonComment(new CommentId(Guid.NewGuid()), content, false, false, DateTime.UtcNow, DateTime.UtcNow, null);
    }

    public void Update(string content)
    {
        Content = content;
        UpdatedDateTime = DateTime.UtcNow;
    }

    public void Complete()
    {
        Completed = true;
        CompletedDateTime = DateTime.UtcNow;
    }

    public void RemoveCompletion()
    {
        Completed = false;
        CompletedDateTime = null;
    }

    public void StrikeThrough()
    {
        StruckThrough = true;
    }

    public void RemoveStrikeThrough()
    {
        StruckThrough = false;
    }
}
